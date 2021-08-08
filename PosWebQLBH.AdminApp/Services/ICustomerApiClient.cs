using PosWebQLBH.ViewModels.Partner.Customers;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public interface ICustomerApiClient
    {
        Task<PagedResult<CustomerVm>> GetCustomerPagings(GetManageCustomerPagingRequest request);

        Task<ApiResult<List<CustomerVm>>> GetAll();

        Task<bool> CreateCustomer(CustomerCreateRequest request);

        Task<bool> UpdateCustomer(CustomerUpdateRequest request);

        Task<ApiResult<CustomerVm>> GetCustomerById(long customerId);

        Task<ApiResult<bool>> DeleteCustomer(long customerId);


    }
}