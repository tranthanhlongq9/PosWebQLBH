using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.Partner.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerApiClient _customerApiClient;
        private readonly IConfiguration _configuration;

        public CustomerController(ICustomerApiClient customerApiClient, IConfiguration configuration)
        {
            _customerApiClient = customerApiClient;
            _configuration = configuration;
        }

        //phương thức lấy customer
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            //lấy session
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageCustomerPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId
            };
            var data = await _customerApiClient.GetCustomerPagings(request);
            ViewBag.Keyword = keyword; //để giữ dữ liệu keyword lại trên view khi tìm kiếm

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMess = TempData["result"];
            }
            return View(data);
        }

        //Tạo -- lấy form nhập
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //-- tạo xong post lên
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CustomerCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _customerApiClient.CreateCustomer(request);
            if (result)
            {
                TempData["result"] = "Thêm mới khách hàng thành công !!";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", "Thêm khách hàng thất bại");
            return View(request);
        }

        //Xóa
        [HttpGet]
        public async Task<IActionResult> Delete(long customerId)
        {
            var result = await _customerApiClient.GetCustomerById(customerId);
            if (result.IsSuccessed)
            {
                var infoCus = result.ResultObj;
                return View(new CustomerDeleteRequest()
                {
                    ID = customerId,
                    Name_Customer = infoCus.Name_Customer,
                    Address = infoCus.Address,
                    Phone_Number = infoCus.Phone_Number,
                });
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CustomerDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _customerApiClient.DeleteCustomer(request.ID);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa khách hàng thành công";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", "Xóa không thành công"); //sẽ thông báo lỗi có message lỗi
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long customerId)
        {
            //var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var result = await _customerApiClient.GetCustomerById(customerId);

            if (result.IsSuccessed)
            {
                var cus = result.ResultObj;
                var editVm = new CustomerUpdateRequest()
                {
                    ID = cus.ID,
                    Name_Customer = cus.Name_Customer,
                    Address = cus.Address,
                    Phone_Number = cus.Phone_Number,
                };
                return View(editVm);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] CustomerUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _customerApiClient.UpdateCustomer(request);
            if (result)
            {
                TempData["result"] = "Cập nhật khách hàng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật thất bại");
            return View(request);
        }

        //chi tiết
        [HttpGet]
        public async Task<IActionResult> Details(long customerId)
        {
            var result = await _customerApiClient.GetCustomerById(customerId);
            return View(result.ResultObj);
        }
    }
}