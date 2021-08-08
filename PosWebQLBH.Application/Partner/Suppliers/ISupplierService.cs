using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.Partner.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Partner.Suppliers
{
    public interface ISupplierService
    {
        //tạo
        Task<string> Create(SupplierCreateRequest request);

        //lấy ncc theo id
        Task<ApiResult<SupplierVm>> GetById(string supplierId);

        //cập nhật
        Task<int> Update(SupplierUpdateRequest request);

        //xóa
        Task<ApiResult<bool>> Delete(string supplierId);

        //lấy tất cả show lên và phân trang
        Task<PagedResult<SupplierVm>> GetAllpaging(GetSupplierPagingRequest request);

        //lấy tất cả 
        Task<List<SupplierVm>> GetAll();
    }
}
