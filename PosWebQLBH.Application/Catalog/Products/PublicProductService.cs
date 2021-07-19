using Microsoft.EntityFrameworkCore;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.ViewModels.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly DbQLBHContext _context;
        public PublicProductService(DbQLBHContext context)
        {
            _context = context;
        }

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
                    Name_Category = x.c.NameCategory,
                    Name_Product = x.p.NameProduct,
                    Price = x.p.Price,
                    Name_Unit = x.u.NameUnit,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Height = x.p.Height,
                    Weight = x.p.Weight
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
    }
}
