using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.Catalog.Categories;
using PosWebQLBH.ViewModels.Catalog.Units;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitApiClient _unitApiClient;
        private readonly IConfiguration _configuration;

        public UnitController(IUnitApiClient unitApiClient, IConfiguration configuration)
        {
            _unitApiClient = unitApiClient;
            _configuration = configuration;
        }

        //phương thức lấy user
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            //lấy session
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetUnitPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId
            };
            var data = await _unitApiClient.GetUnitPagings(request);
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
        public async Task<IActionResult> Create([FromForm] UnitCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _unitApiClient.CreateUnit(request);
            if (result)
            {
                TempData["result"] = "Thêm mới đơn vị tính thành công !!";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", "Thêm đơn vị tính thất bại");
            return View(request);
        }
     
        //chi tiết
        [HttpGet]
        public async Task<IActionResult> Details(string unitId)
        {
            var result = await _unitApiClient.GetUnitById(unitId);
            return View(result.ResultObj);
        }

        //Xóa 
        [HttpGet]
        public async Task<IActionResult> Delete(string unitId)
        {
            var result = await _unitApiClient.GetUnitById(unitId);
            if (result.IsSuccessed)
            {
                var infoUnit = result.ResultObj;
                return View(new UnitDeleteRequest()
                {
                    ID_Unit = unitId,
                    Name_Unit = infoUnit.Name_Unit
                });
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UnitDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _unitApiClient.DeleteUnit(request.ID_Unit);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa đơn vị tính thành công";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", "Xóa không thành công"); //sẽ thông báo lỗi có message lỗi
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string unitId)
        {
            //var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var result = await _unitApiClient.GetUnitById(unitId);

            if (result.IsSuccessed)
            {
                var unit = result.ResultObj;
                var editVm = new UnitUpdateRequest()
                {
                    ID_Unit = unit.ID_Unit,
                    Name_Unit = unit.Name_Unit,
                };
                return View(editVm);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] UnitUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _unitApiClient.UpdateUnit(request);
            if (result)
            {
                TempData["result"] = "Cập nhật đơn vị tính thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật đơn vị tính thất bại");
            return View(request);
        }
    }
}
