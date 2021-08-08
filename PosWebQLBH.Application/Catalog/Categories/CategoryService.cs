using Microsoft.EntityFrameworkCore;
using PosWebQLBH.Application.Common;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.Utilities.Exceptions;
using PosWebQLBH.ViewModels.Catalog.Categories;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        //khai báo
        private readonly DbQLBHContext _context;

        private readonly IStorageService _storageService;

        //gán vào constructor
        public CategoryService(DbQLBHContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        //tạo
        public async Task<string> Create(CategoryCreateRequest request)
        {
            var category = new Category()
            {
                IdCategory = request.ID_Category,
                NameCategory = request.Name_Category,
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.Now,
                UpdatedBy = request.UpdatedBy,
                UpdatedDate = DateTime.Now,


            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category.IdCategory;
        }

        //xóa ngành hàng
        public async Task<ApiResult<bool>> Delete(string categoryId)
        {
            var cate = await _context.Categories.FindAsync(categoryId);
            if (cate == null) throw new EShopException($"Cannot find a category: {categoryId}");

            _context.Categories.Remove(cate);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        //lấy tất cả
        public async Task<List<CategoryVm>> GetAll() //sau này thêm ngôn ngữ thì thêm 1 biến (string languageId)
        {
            var query = from c in _context.Categories
                        select new { c };

            var data= await query.Select(x => new CategoryVm()
            {
                ID_Catetory = x.c.IdCategory,
                Name_Catetory = x.c.NameCategory
            }).ToListAsync();

            return data;
        }

        //show tất cả và phân trang
        public async Task<PagedResult<CategoryVm>> GetAllpaging(GetCategoryPagingRequest request)
        {
            //1. select join
            var query = from cate in _context.Categories
                            //join sell in _context.SellOrders on cus.IdCustomer equals sell.IdCustomer
                            //select new { cus, sell};
                        select new { cate };

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.cate.NameCategory.Contains(request.Keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CategoryVm()
                {
                    ID_Catetory = x.cate.IdCategory,
                    Name_Catetory = x.cate.NameCategory,
                    CreatedBy = x.cate.CreatedBy,
                    CreatedDate = x.cate.CreatedDate,
                    UpdatedBy = x.cate.UpdatedBy,
                    UpdatedDate = x.cate.UpdatedDate

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<CategoryVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        //hàm lấy ngành hàng theo id
        public async Task<ApiResult<CategoryVm>> GetById(string categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) throw new EShopException($"Cannot find a category: {categoryId}");

            var categoryViewModel = new CategoryVm()
            {
                ID_Catetory = category.IdCategory,
                Name_Catetory = category != null ? category.NameCategory : null,

                CreatedBy = category != null ? category.CreatedBy : null,
                CreatedDate = category.CreatedDate,
                UpdatedBy = category != null ? category.UpdatedBy : null,
                UpdatedDate = category.UpdatedDate,

            };
            return new ApiSuccessResult<CategoryVm>(categoryViewModel);
        }

        //hàm cập nhật 
        public async Task<int> Update(CategoryUpdateRequest request)
        {
            var category = await _context.Categories.FindAsync(request.ID_Category);
            if (category == null) throw new EShopException($"Cannot find a category with id: {request.ID_Category}");

            category.IdCategory = request.ID_Category;
            category.NameCategory = request.Name_Category;
            category.UpdatedBy = request.UpdatedBy;
            category.UpdatedDate = DateTime.Now;

            return await _context.SaveChangesAsync();

        }
    }
}