using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PosWebQLBH.Application.Common;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.Utilities.Exceptions;
using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.Partner.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Partner.Customers
{
    public class CustomerService : ICustomerService
    {
        //khai báo
        private readonly DbQLBHContext _context;

        private readonly IStorageService _storageService;

        //gán
        public CustomerService(DbQLBHContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        //hàm tạo
        public async Task<long> Create(CustomerCreateRequest request)
        {
            var customer = new Customer()
            {
                //IdCustomer = request.ID_Customer,
                NameCustomer = request.Name_Customer,
                PhoneNumber = request.Phone_Number,
                Address = request.Address,
                CreatedBy = request.CreatedBy,
                CreatedDate = DateTime.Now,
                UpdatedBy = request.UpdatedBy,
                UpdatedDate = DateTime.Now,
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.IdCustomer;
        }

        //hàm lấy khách hàng theo id
        public async Task<ApiResult<CustomerVm>> GetById(long customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) throw new EShopException($"Cannot find a customer: {customerId}");

            //var sellorder = await _context.SellOrders.FirstOrDefaultAsync(x => x.IdCustomer == customerId);

            var customerVm = new CustomerVm()
            {
                ID = customer.IdCustomer,
                Name_Customer = customer != null ? customer.NameCustomer : null,
                Address = customer != null ? customer.Address : null,
                Phone_Number = customer != null ? customer.PhoneNumber : null,
                CreatedBy = customer != null ? customer.CreatedBy : null,
                CreatedDate = customer.CreatedDate,
                UpdatedBy = customer != null ? customer.UpdatedBy : null,
                UpdatedDate = customer.UpdatedDate,
                //ID_Sell = sellorder.IdSellOrder ,
            };
            return new ApiSuccessResult<CustomerVm>(customerVm);
        }

        //hàm lấy tất cả khách hàng
        public async Task<List<CustomerVm>> GetAll()
        {
            var query = from cus in _context.Customers
                            //join sell in _context.SellOrders on cus.IdCustomer equals sell.IdCustomer
                            //select new { cus, sell};
                        select new { cus };

            var data = await query.Select(x => new CustomerVm()
            {
                ID = x.cus.IdCustomer,
                Name_Customer = x.cus.NameCustomer,
                Address = x.cus.Address,
                Phone_Number = x.cus.PhoneNumber,
                CreatedBy = x.cus.CreatedBy,
                CreatedDate = x.cus.CreatedDate,
                UpdatedBy = x.cus.UpdatedBy,
                UpdatedDate = x.cus.UpdatedDate,

                //ID_Sell = x.sell.IdSellOrder,
            }).ToListAsync();
            return data;
        }

        //hàm lấy tất cả khách hàng phân trang
        public async Task<PagedResult<CustomerVm>> GetAllpaging(GetManageCustomerPagingRequest request)
        {
            //1. select join
            var query = from cus in _context.Customers
                            //join sell in _context.SellOrders on cus.IdCustomer equals sell.IdCustomer
                            //select new { cus, sell};
                        select new { cus };

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.cus.NameCustomer.Contains(request.Keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CustomerVm()
                {
                    ID = x.cus.IdCustomer,
                    Name_Customer = x.cus.NameCustomer,
                    Address = x.cus.Address,
                    Phone_Number = x.cus.PhoneNumber,
                    CreatedBy = x.cus.CreatedBy,
                    CreatedDate = x.cus.CreatedDate,
                    UpdatedBy = x.cus.UpdatedBy,
                    UpdatedDate = x.cus.UpdatedDate
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<CustomerVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        //hàm cập nhật
        public async Task<int> Update(CustomerUpdateRequest request)
        {
            var cus = await _context.Customers.FindAsync(request.ID);
            if (cus == null) throw new EShopException($"Cannot find a unit with id: {request.ID}");

            //cus.IdCustomer = request.ID;
            cus.NameCustomer = request.Name_Customer;
            cus.Address = request.Address;
            cus.PhoneNumber = request.Phone_Number;
            cus.UpdatedBy = request.UpdatedBy;
            cus.UpdatedDate = DateTime.Now;

            return await _context.SaveChangesAsync();
        }

        //hàm xóa dvt
        public async Task<ApiResult<bool>> Delete(long customerId)
        {
            var cus = await _context.Customers.FindAsync(customerId);
            if (cus == null) throw new EShopException($"Cannot find a customer : {customerId}");

            _context.Customers.Remove(cus);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }
    }
}