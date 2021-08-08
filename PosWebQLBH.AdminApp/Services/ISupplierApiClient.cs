using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.Partner.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public interface ISupplierApiClient
    {
        Task<PagedResult<SupplierVm>> GetSupplierPagings(GetSupplierPagingRequest request);

        Task<bool> CreateSupplier(SupplierCreateRequest request);

        Task<ApiResult<List<SupplierVm>>> GetAll();

        Task<bool> UpdateSupplier(SupplierUpdateRequest request);

        Task<ApiResult<SupplierVm>> GetSupplierById(string supplierId);

        Task<ApiResult<bool>> DeleteSupplier(string supplierId);
    }
}
