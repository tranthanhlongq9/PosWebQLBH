using PosWebQLBH.ViewModels.Catalog.Units;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public interface IUnitApiClient
    {
        Task<PagedResult<UnitVm>> GetUnitPagings(GetUnitPagingRequest request);

        Task<bool> CreateUnit(UnitCreateRequest request);

        Task<ApiResult<List<UnitVm>>> GetAll();

        Task<bool> UpdateUnit(UnitUpdateRequest request);

        Task<ApiResult<UnitVm>> GetUnitById(string unitId);

        Task<ApiResult<bool>> DeleteUnit(string unitId);
    }
}
