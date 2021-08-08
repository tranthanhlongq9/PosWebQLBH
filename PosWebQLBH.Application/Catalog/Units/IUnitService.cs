using PosWebQLBH.ViewModels.Catalog.Units;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Units
{
    public interface IUnitService
    {
        //tạo
        Task<string> Create(UnitCreateRequest request);

        //cập nhật
        Task<int> Update(UnitUpdateRequest request);

        //xóa
        Task<ApiResult<bool>> Delete(string unitId);

        //lấy theo id
        Task<ApiResult<UnitVm>> GetById(string unitId);

        //lấy tất cả show lên và phân trang
        Task<PagedResult<UnitVm>> GetAllpaging(GetUnitPagingRequest request);

        //lấy tất cả 
        Task<List<UnitVm>> GetAll();
    }
}
