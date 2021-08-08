using PosWebQLBH.ViewModels.Catalog.Categories;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryVm>> GetAll();

        Task<PagedResult<CategoryVm>> GetCategoryPagings(GetCategoryPagingRequest request);

        Task<bool> CreateCategory(CategoryCreateRequest request);

        //Task<ApiResult<List<CategoryVm>>> GetAll();

        Task<bool> UpdateCategory(CategoryUpdateRequest request);

        Task<ApiResult<bool>> DeleteCategory(string categoryId);

        Task<ApiResult<CategoryVm>> GetCategoryById(string categoryId);
    }
}