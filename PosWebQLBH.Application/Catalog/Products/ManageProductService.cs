using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PosWebQLBH.Application.Common;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.Utilities.Exceptions;
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
    class ManageProductService : IManageProductService
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
                        Quantity = request.Quantity
                    }
                }

            };
            //save image
            //if (request.ThumbnailImage != null)
            //{
            //    product = new Product()
            //    {
            //        ImagePath = await this.SaveFile(request.ThumbnailImage),

            //    };                              
            //}

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.IdProduct;

        }

        public async Task<int> Delete(string productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product: {productId}");

            //xóa ảnh
            //var images = _context.Products.Where(i => i.ID_Product == productId);
            //foreach (var image in images)
            //{
            //    await _storageService.DeleteFileAsync(image.ImagePath);
            //}

            //xóa bên bảng Inventory
            var quantity = await _context.Inventories.FindAsync(productId);
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
                    ID_Product = x.p.IdProduct,
                    Nam_Category = x.c.NameCategory,
                    Name_Product = x.p.NameProduct,
                    Price = x.p.Price,
                    Name_Unit = x.u.NameUnit,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Height = x.p.Height,
                    Weight = x.p.Weight,
                    //Quantity = x.inv.Quantity,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;

        }

        public Task<ProductViewModel> GetById(string productId)
        {
            throw new NotImplementedException();
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

            //update tồn kho
            var quantity = await _context.Inventories.FindAsync(request.ID_Product);
            quantity.Quantity = request.Quantity;
            _context.Inventories.Update(quantity);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product with id: {productId}");
            product.Price = newPrice;
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
