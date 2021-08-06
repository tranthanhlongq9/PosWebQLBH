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
    public class ProductService : IProductService
    {
        //khai báo
        private readonly DbQLBHContext _context;

        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        //gán
        public ProductService(DbQLBHContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        //hàm tạo sp
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
                //UpdatedBy = request.UpdatedBy,
                //UpdatedDate = DateTime.Now,

                //save image
                ImagePath = await this.SaveFile(request.ThumbnailImage),

                Inventories = new List<Inventory>()
                {
                    new Inventory()
                    {
                        Quantity = request.Quantity,
                        CreatedBy = request.CreatedBy,
                        CreatedDate = DateTime.Now,
                        
                    }
                }
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.IdProduct;
        }

        //hàm xóa sp
        public async Task<ApiResult<bool>> Delete(string productId)
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
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        //hàm lấy sp và sắp xếp show theo trang
        public async Task<PagedResult<ProductViewModel>> GetAllpaging(GetManageProductPagingRequest request)
        {
            //1. select join
            var query = from p in _context.Products
                        join inv in _context.Inventories on p.IdProduct equals inv.IdProduct
                        join c in _context.Categories on p.IdCategory equals c.IdCategory
                        join u in _context.Units on p.IdUnit equals u.IdUnit
                        //where p.LanguageId == request.LanguageId //sau này thêm language sẽ cần
                        select new { p, c, inv, u };

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.NameProduct.Contains(request.Keyword));

            if (request.CategoryId != null)
            {
                query = query.Where(p => p.c.IdCategory == request.CategoryId);
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
                    CreatedBy = x.p.CreatedBy,
                    CreatedDate = x.p.CreatedDate,
                    UpdatedBy = x.p.UpdatedBy,
                    UpdatedDate = x.p.UpdatedDate,
                    ThumbnailImage = x.p.ImagePath
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        //hàm lấy sp theo id
        public async Task<ApiResult<ProductViewModel>> GetById(string productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product: {productId}");

            var inventory = await _context.Inventories.FirstOrDefaultAsync(x => x.IdProduct == productId);
            var cate = await _context.Categories.FindAsync(product.IdCategory);
            var unit = await _context.Units.FindAsync(product.IdUnit);

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
            return new ApiSuccessResult<ProductViewModel>(productViewModel);
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

        //hàm cập nhật sản phẩm
        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.ID_Product);
            if (product == null) throw new EShopException($"Cannot find a product with id: {request.ID_Product}");

            product.IdProduct = request.ID_Product;
            product.IdCategory = request.ID_Category;
            product.NameProduct = request.Name_Product;
            product.Price = request.Price;
            product.IdUnit = request.ID_Unit;
            product.Length = request.Length;
            product.Width = request.Width;
            product.Height = request.Height;
            product.Weight = request.Weight;
            product.UpdatedBy = request.UpdatedBy;
            product.UpdatedDate = DateTime.Now;
            //save image
            if (request.ThumbnailImage != null)
            {
                ////xóa ảnh
                var images = _context.Products.Where(i => i.IdProduct == request.ID_Product);
                foreach (var image in images)
                {
                    await _storageService.DeleteFileAsync(image.ImagePath);
                }
                //thêm ảnh mới
                product.ImagePath = await this.SaveFile(request.ThumbnailImage);
            }

            //update SL
            var quantity = await _context.Inventories.FirstOrDefaultAsync(x => x.IdProduct == request.ID_Product);
            quantity.Quantity = request.Quantity;
            quantity.UpdatedBy = request.UpdatedBy;
            quantity.UpdatedDate = DateTime.Now;
            _context.Inventories.Update(quantity);

            return await _context.SaveChangesAsync();
        }

        //hàm cập nhật giá
        public async Task<bool> UpdatePrice(string productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product with id: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        //hàm cập nhật tồn kho
        public async Task<bool> UpdateStock(string productId, int addedQuantity) //update số lượng tồn kho
        {
            var proQuantity = await _context.Inventories.FirstOrDefaultAsync(x => x.IdProduct == productId);
            if (proQuantity == null) throw new EShopException($"Cannot find a product with id: {productId}");
            proQuantity.Quantity += addedQuantity; //tăng số lượng tồn kho lên
            return await _context.SaveChangesAsync() > 0;
        }

        //hàm lấy tất cả sp
        public async Task<List<ProductViewModel>> GetAll()
        {
            //return await _context.Products.Select(x => new ProductViewModel()
            //{
            //    ID_Product = x.IdProduct,
            //    Name_Product = x.NameProduct,

            //}).ToListAsync();

            var query = from p in _context.Products
                        join inv in _context.Inventories on p.IdProduct equals inv.IdProduct
                        join c in _context.Categories on p.IdCategory equals c.IdCategory
                        join u in _context.Units on p.IdUnit equals u.IdUnit
                        select new { p, c, u, inv };

            var data = await query.Select(x => new ProductViewModel()
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
                CreatedBy = x.p.CreatedBy,
                CreatedDate = x.p.CreatedDate,
                UpdatedBy = x.p.UpdatedBy,
                UpdatedDate = x.p.UpdatedDate,
                ThumbnailImage = x.p.ImagePath
            }).ToListAsync();
            return data;
        }

        //hàm lấy sản phẩm theo category id
        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            //1. select join
            var query = from p in _context.Products
                        join inv in _context.Inventories on p.IdProduct equals inv.IdProduct
                        join c in _context.Categories on p.IdCategory equals c.IdCategory
                        join u in _context.Units on p.IdUnit equals u.IdUnit
                        select new { p, c, inv, u };

            //2. filter
            if (request.CategoryId != null)
            {
                query = query.Where(p => p.p.IdCategory == request.CategoryId);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ID = x.p.IdProduct,
                    ID_Category = x.p.IdCategory,
                    Name_Category = x.c.NameCategory,
                    Name_Product = x.p.NameProduct,
                    Price = x.p.Price,
                    ID_Unit = x.p.IdUnit,
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
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        //Hàm save file ảnh
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
            //return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}