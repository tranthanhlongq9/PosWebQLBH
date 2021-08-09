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

namespace PosWebQLBH.WebPOS.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly ICustomerApiClient _customerApiClient;
        private readonly IConfiguration _configuration;

        public KhachHangController(ICustomerApiClient customerApiClient, IConfiguration configuration)
        {
            _customerApiClient = customerApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 20)
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
    }
}