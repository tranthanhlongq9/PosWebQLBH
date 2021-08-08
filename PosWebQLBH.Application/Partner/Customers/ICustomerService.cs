using System.Collections.Generic;
using System.Threading.Tasks;
using PosWebQLBH.ViewModels.Partner.Customers;
using PosWebQLBH.ViewModels.Common;

namespace PosWebQLBH.Application.Partner.Customers
{
    public interface ICustomerService
    {
        //tạo
        Task<long> Create(CustomerCreateRequest request);

        //cập nhật
        Task<int> Update(CustomerUpdateRequest request);

        //xóa
        Task<ApiResult<bool>> Delete(long customerId);

        //lấy theo id
        Task<ApiResult<CustomerVm>> GetById(long customerId);

        //lấy tất cả KH
        Task<List<CustomerVm>> GetAll();

        //lấy tất cả show lên và phân trang
        Task<PagedResult<CustomerVm>> GetAllpaging(GetManageCustomerPagingRequest request);
    }
}
