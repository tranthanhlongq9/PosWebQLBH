using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.Partner.Suppliers;

using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierApiClient _supplierApiClient;
        private readonly IConfiguration _configuration;

        public SupplierController(ISupplierApiClient supplierApiClient, IConfiguration configuration)
        {
            _supplierApiClient = supplierApiClient;
            _configuration = configuration;
        }

        //phương thức lấy user
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            //lấy session
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetSupplierPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId
            };
            var data = await _supplierApiClient.GetSupplierPagings(request);
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
        public async Task<IActionResult> Create([FromForm] SupplierCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _supplierApiClient.CreateSupplier(request);
            if (result)
            {
                TempData["result"] = "Thêm mới nhà cung cấp thành công !!";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", "Thêm nhà cung cấp thất bại");
            return View(request);
        }

        //chi tiết
        [HttpGet]
        public async Task<IActionResult> Details(string supplierId)
        {
            var result = await _supplierApiClient.GetSupplierById(supplierId);
            return View(result.ResultObj);
        }

        //Xóa 
        [HttpGet]
        public async Task<IActionResult> Delete(string SupplierId)
        {
            var result = await _supplierApiClient.GetSupplierById(SupplierId);
            if (result.IsSuccessed)
            {
                var infoSupliier = result.ResultObj;
                return View(new SupplierDeleteRequest()
                {
                    ID_Supplier = SupplierId,
                    Name_Supplier = infoSupliier.Name_Supplier,
                    Address = infoSupliier.Address,
                    Representative = infoSupliier.Representative,
                    Phone_Number = infoSupliier.Phone_Number,
                });
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SupplierDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _supplierApiClient.DeleteSupplier(request.ID_Supplier);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa nhà cung cấp  thành công";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", "Xóa không thành công"); //sẽ thông báo lỗi có message lỗi
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string supplierId)
        {
            //var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var result = await _supplierApiClient.GetSupplierById(supplierId);

            if (result.IsSuccessed)
            {
                var sup = result.ResultObj;
                var editVm = new SupplierUpdateRequest()
                {
                    ID_Supplier = sup.ID_Supplier,
                    Name_Supplier = sup.Name_Supplier,
                    Address = sup.Address,
                    Representative = sup.Representative,
                    Phone_Number = sup.Phone_Number,
                };
                return View(editVm);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] SupplierUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _supplierApiClient.UpdateSupplier(request);
            if (result)
            {
                TempData["result"] = "Cập nhật nhà cung cấp thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật thất bại");
            return View(request);
        }
    }
}
