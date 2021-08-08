using PosWebQLBH.ViewModels.Catalog.Categories;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        //tạo
        Task<string> Create(CategoryCreateRequest request);

        //cập nhật
        Task<int> Update(CategoryUpdateRequest request);

        ////xóa
        //Task<string> Delete(string categoryId);
        Task<ApiResult<bool>> Delete(string categoryId);

        //lấy theo id
        //Task<CategoryVm> GetById(string categoryId);
        Task<ApiResult<CategoryVm>> GetById(string categoryId);


        //lấy tất cả show lên và phân trang
        Task<PagedResult<CategoryVm>> GetAllpaging(GetCategoryPagingRequest request);

        //lấy tất cả 
        Task<List<CategoryVm>> GetAll();
    }
}