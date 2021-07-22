using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PosWebQLBH.Application.Common;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.Utilities.Exceptions;
using PosWebQLBH.ViewModels.Catalog.ProductImages;
using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly DbQLBHContext _context;
        private readonly IStorageService _storageService;

        public ManageProductService(DbQLBHContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<string> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                IdProduct = request.ID_Product,
                IdUnit = request.ID_Unit,
                IdCategory = request.ID_Category,
                NameProduct = request.Name_Product,
                Price = request.Price,
                Length = request.Length,
                Width = request.Width,
                Height = request.Height,
                Weight = request.Weight,
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.Now,
                UpdatedBy = request.UpdatedBy,
                UpdatedDate = DateTime.Now,

                //save image
                ImagePath = await this.SaveFile(request.ThumbnailImage),

                Inventories = new List<Inventory>()
                {
                    new Inventory()
                    {
                        Quantity = request.Quantity,
                        CreatedBy = request.CreatedBy,
                        CreatedDate = DateTime.Now,
                        UpdatedBy = request.UpdatedBy,
                        UpdatedDate = DateTime.Now,
                    }
                }
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.IdProduct;
        }

        public async Task<int> Delete(string productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product: {productId}");

            //xóa ảnh
            var images = _context.Products.Where(i => i.IdProduct == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            //xóa bên bảng Inventory
            var quantity = await _context.Inventories.FirstOrDefaultAsync(x => x.IdProduct == productId);
            _context.Inventories.Remove(quantity);

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllpaging(GetManageProductPagingRequest request)
        {
            //1. select join
            var query = from p in _context.Products
                        join inv in _context.Inventories on p.IdProduct equals inv.IdProduct
                        join c in _context.Categories on p.IdCategory equals c.IdCategory
                        join u in _context.Units on p.IdUnit equals u.IdUnit
                        select new { p, c, inv, u };

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.NameProduct.Contains(request.Keyword));

            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.c.IdCategory));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ID = x.p.IdProduct,
                    Name_Category = x.c.NameCategory,
                    Name_Product = x.p.NameProduct,
                    Price = x.p.Price,
                    Name_Unit = x.u.NameUnit,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Height = x.p.Height,
                    Weight = x.p.Weight,
                    Quantity = x.inv.Quantity,
                    ThumbnailImage = x.p.ImagePath
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ProductViewModel> GetById(string productId)
        {
            var product = await _context.Products.FindAsync(productId);
            var inventory = await _context.Inventories.FirstOrDefaultAsync(x => x.IdProduct == productId);
            var cate = await _context.Categories.FindAsync(product.IdCategory);
            var unit = await _context.Units.FindAsync(product.IdUnit);

            //if (product == null || inventory == null || cate == null || unit == null) throw new EShopException($"Cannot find a product: {productId}");

            var productViewModel = new ProductViewModel()
            {
                ID = product.IdProduct,
                Name_Product = product != null ? product.NameProduct : null,
                ID_Category = product != null ? product.IdCategory : null,
                Name_Category = cate.NameCategory,
                Price = product.Price,
                ID_Unit = product != null ? product.IdUnit : null,
                Name_Unit = unit.NameUnit,
                Length = product.Length,
                Width = product.Width,
                Height = product.Height,
                Weight = product.Weight,
                CreatedBy = product != null ? product.CreatedBy : null,
                CreatedDate = product.CreatedDate,
                UpdatedBy = product != null ? product.UpdatedBy : null,
                UpdatedDate = product.UpdatedDate,
                Quantity = inventory.Quantity,
                ThumbnailImage = product != null ? product.ImagePath : null
            };
            return productViewModel;
        }

        //Phát  triển sau
        public async Task<List<ProductImageViewModel>> GetListImages(string productId)
        {
            return await _context.Products.Where(x => x.IdProduct == productId)
                .Select(i => new ProductImageViewModel()
                {
                    ProductId = i.IdProduct,
                    ImagePath = i.ImagePath,
                    //SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.ID_Product);
            if (product == null) throw new EShopException($"Cannot find a product with id: {request.ID_Product}");

            product.IdProduct = request.ID_Product;
            product.IdCategory = request.ID_Category;
            product.NameProduct = request.Name_Product;
            product.IdUnit = request.ID_Unit;
            product.Length = request.Length;
            product.Width = request.Width;
            product.Height = request.Height;
            product.Weight = request.Weight;
            //save image
            product.ImagePath = await this.SaveFile(request.ThumbnailImage);

            //update SL
            var quantity = await _context.Inventories.FirstOrDefaultAsync(x => x.IdProduct == request.ID_Product);
            quantity.Quantity = request.Quantity;
            _context.Inventories.Update(quantity);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(string productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product with id: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(string productId, int addedQuantity) //update số lượng tồn kho
        {
            var proQuantity = await _context.Inventories.FirstOrDefaultAsync(x => x.IdProduct == productId);
            if (proQuantity == null) throw new EShopException($"Cannot find a product with id: {productId}");
            proQuantity.Quantity += addedQuantity; //tăng số lượng tồn kho lên
            return await _context.SaveChangesAsync() > 0;
        }

        //Hàm save file ảnh
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}