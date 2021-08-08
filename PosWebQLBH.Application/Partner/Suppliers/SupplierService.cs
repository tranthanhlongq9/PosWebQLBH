using Microsoft.EntityFrameworkCore;
using PosWebQLBH.Application.Common;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.Utilities.Exceptions;
using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.Partner.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Partner.Suppliers
{
    public class SupplierService : ISupplierService
    {
        //khai báo
        private readonly DbQLBHContext _context;
        private readonly IStorageService _storageService;


        //gán 
        public SupplierService(DbQLBHContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        //tạo
        public async Task<string> Create(SupplierCreateRequest request)
        {
            var supplier = new Supplier()
            {
                IdSupplier = request.ID_Supplier,
                NameSupplier = request.Name_Supplier,
                PhoneNumber = request.Phone_Number,
                Representative = request.Representative,
                Address = request.Address,
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.Now,
                UpdatedBy = request.UpdatedBy,
                UpdatedDate = DateTime.Now,

                
            };
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier.IdSupplier;
        }

        //show tất cả và phân trang
        public async Task<PagedResult<SupplierVm>> GetAllpaging(GetSupplierPagingRequest request)
        {
            //1. select join
            var query = from sup in _context.Suppliers
                            //join sell in _context.SellOrders on cus.IdCustomer equals sell.IdCustomer
                            //select new { cus, sell};
                        select new { sup };

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.sup.NameSupplier.Contains(request.Keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new SupplierVm()
                {
                    ID_Supplier = x.sup.IdSupplier,
                    Name_Supplier = x.sup.NameSupplier,
                    Representative = x.sup.Representative,
                    Address = x.sup.Address,
                    Phone_Number = x.sup.PhoneNumber,
                    CreatedBy = x.sup.CreatedBy,
                    CreatedDate = x.sup.CreatedDate,
                    UpdatedBy = x.sup.UpdatedBy,
                    UpdatedDate = x.sup.UpdatedDate

                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<SupplierVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        //hàm lấy nhà cung cấp  theo id
        public async Task<ApiResult<SupplierVm>> GetById(string supplierId)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier == null) throw new EShopException($"Cannot find a supplier: {supplierId}");

            var supplierViewModel = new SupplierVm()
            {
                ID_Supplier = supplier.IdSupplier,
                Name_Supplier = supplier != null ? supplier.NameSupplier : null,
                Address = supplier != null ? supplier.Address : null,
                Representative= supplier != null ? supplier.Representative : null,
                Phone_Number = supplier != null ? supplier.PhoneNumber : null,

                CreatedBy = supplier != null ? supplier.CreatedBy : null,
                CreatedDate = supplier.CreatedDate,
                UpdatedBy = supplier != null ? supplier.UpdatedBy : null,
                UpdatedDate = supplier.UpdatedDate,
               
            };
            return new ApiSuccessResult<SupplierVm>(supplierViewModel);
        }

        //hàm lấy tất cả 
        public async Task<List<SupplierVm>> GetAll()
        {
            var query = from sup in _context.Suppliers
                            //join sell in _context.SellOrders on cus.IdCustomer equals sell.IdCustomer
                            //select new { cus, sell};
                        select new { sup };

            var data = await query.Select(x => new SupplierVm()
            {
                ID_Supplier = x.sup.IdSupplier,
                Name_Supplier = x.sup.NameSupplier,
                Representative = x.sup.Representative,
                Address = x.sup.Address,
                Phone_Number = x.sup.PhoneNumber,
                CreatedBy = x.sup.CreatedBy,
                CreatedDate = x.sup.CreatedDate,
                UpdatedBy = x.sup.UpdatedBy,
                UpdatedDate = x.sup.UpdatedDate


                //ID_Sell = x.sell.IdSellOrder,


            }).ToListAsync();
            return data;
        }

        //hàm cập nhật 
        public async Task<int> Update(SupplierUpdateRequest request)
        {
            var sup = await _context.Suppliers.FindAsync(request.ID_Supplier);
            if (sup == null) throw new EShopException($"Cannot find a unit with id: {request.ID_Supplier}");

            sup.IdSupplier = request.ID_Supplier;
            sup.NameSupplier = request.Name_Supplier;
            sup.Representative = request.Representative;
            sup.PhoneNumber = request.Phone_Number;
            sup.Address = request.Address;

            sup.UpdatedBy = request.UpdatedBy;
            sup.UpdatedDate = DateTime.Now;

            return await _context.SaveChangesAsync();

        }

        //hàm xóa ncc
        public async Task<ApiResult<bool>> Delete(string supplierId)
        {
            var sup = await _context.Suppliers.FindAsync(supplierId);
            if (sup == null) throw new EShopException($"Cannot find a unit: {supplierId}");

            _context.Suppliers.Remove(sup);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }
    }
}
