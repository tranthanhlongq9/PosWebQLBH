using Microsoft.EntityFrameworkCore;
using PosWebQLBH.Application.Common;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.Utilities.Exceptions;
using PosWebQLBH.ViewModels.Catalog.Units;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Units
{
    public class UnitService : IUnitService
    {
        //khai báo
        private readonly DbQLBHContext _context;

        private readonly IStorageService _storageService;

        //gán 
        public UnitService(DbQLBHContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        //tạo
        public async Task<string> Create(UnitCreateRequest request)
        {
            var unit = new Unit()
            {
                IdUnit = request.ID_Unit,
                NameUnit = request.Name_Unit,
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.Now,
                UpdatedBy = request.UpdatedBy,
                UpdatedDate = DateTime.Now,


            };
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
            return unit.IdUnit;
        }

        //show tất cả và phân trang
        public async Task<PagedResult<UnitVm>> GetAllpaging(GetUnitPagingRequest request)
        {
            //1. select join
            var query = from u in _context.Units
                            //join sell in _context.SellOrders on cus.IdCustomer equals sell.IdCustomer
                            //select new { cus, sell};
                        select new { u };

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.u.NameUnit.Contains(request.Keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UnitVm()
                {
                    ID_Unit = x.u.IdUnit,
                    Name_Unit = x.u.NameUnit,
                    CreatedBy = x.u.CreatedBy,
                    CreatedDate = x.u.CreatedDate,
                    UpdatedBy = x.u.UpdatedBy,
                    UpdatedDate = x.u.UpdatedDate

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UnitVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        //hàm lấy nhà cung cấp  theo id
        public async Task<ApiResult<UnitVm>> GetById(string unitId)
        {
            var unit = await _context.Units.FindAsync(unitId);
            if (unit == null) throw new EShopException($"Cannot find a unit: {unitId}");

            var unitViewModel = new UnitVm()
            {
                ID_Unit = unit.IdUnit,
                Name_Unit = unit != null ? unit.NameUnit : null,

                CreatedBy = unit != null ? unit.CreatedBy : null,
                CreatedDate = unit.CreatedDate,
                UpdatedBy = unit != null ? unit.UpdatedBy : null,
                UpdatedDate = unit.UpdatedDate,

            };
            return new ApiSuccessResult<UnitVm>(unitViewModel);
        }

        //hàm lấy tất cả 
        public async Task<List<UnitVm>> GetAll()
        {
            var query = from u in _context.Units
                            //join sell in _context.SellOrders on cus.IdCustomer equals sell.IdCustomer
                            //select new { cus, sell};
                        select new { u };

            var data = await query.Select(x => new UnitVm()
            {
                ID_Unit = x.u.IdUnit,
                Name_Unit = x.u.NameUnit,
                CreatedBy = x.u.CreatedBy,
                CreatedDate = x.u.CreatedDate,
                UpdatedBy = x.u.UpdatedBy,
                UpdatedDate = x.u.UpdatedDate


                //ID_Sell = x.sell.IdSellOrder,


            }).ToListAsync();
            return data;
        }

        //hàm cập nhật 
        public async Task<int> Update(UnitUpdateRequest request)
        {
            var unit = await _context.Units.FindAsync(request.ID_Unit);
            if (unit == null) throw new EShopException($"Cannot find a unit with id: {request.ID_Unit}");

            unit.IdUnit = request.ID_Unit;
            unit.NameUnit = request.Name_Unit;
            unit.UpdatedBy = request.UpdatedBy;
            unit.UpdatedDate = DateTime.Now;

           return  await _context.SaveChangesAsync();

        }

        //hàm xóa dvt
        public async Task<ApiResult<bool>> Delete(string unitId)
        {
            var unit = await _context.Units.FindAsync(unitId);
            if (unit == null) throw new EShopException($"Cannot find a unit: {unitId}");

            _context.Units.Remove(unit);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }
    }

}
