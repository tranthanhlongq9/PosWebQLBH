using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IConfiguration _configuration;

        public CategoryController(ICategoryApiClient categoryApiClient, IConfiguration configuration)
        {
            _categoryApiClient = categoryApiClient;
            _configuration = configuration;
        }

        //show lên
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            //lấy session
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetCategoryPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId
            };
            var data = await _categoryApiClient.GetCategoryPagings(request);
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
        public async Task<IActionResult> Create([FromForm] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _categoryApiClient.CreateCategory(request);
            if (result)
            {
                TempData["result"] = "Thêm mới ngành hàng thành công !!";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", "Thêm ngành hàng thất bại");
            return View(request);
        }

        //chi tiết
        [HttpGet]
        public async Task<IActionResult> Details(string categoryId)
        {
            var result = await _categoryApiClient.GetCategoryById(categoryId);
            return View(result.ResultObj);
        }

        //Xóa ngành hàng
        [HttpGet]
        public async Task<IActionResult> Delete(string categoryId)
        {
            var result = await _categoryApiClient.GetCategoryById(categoryId);
            if (result.IsSuccessed)
            {
                var infoCategory = result.ResultObj;
                return View(new CategoryDeleteRequest()
                {
                    ID_Category = categoryId,
                    Name_Category = infoCategory.Name_Catetory
                });
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        //public async Task<IActionResult> Delete(CategoryDeleteRequest request)
        public async Task<IActionResult> Delete(CategoryDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoryApiClient.DeleteCategory(request.ID_Category);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa ngành hàng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }

        //cập nhật 
        [HttpGet]
        public async Task<IActionResult> Edit(string categoryId)
        {
            //var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var result = await _categoryApiClient.GetCategoryById(categoryId);

            if (result.IsSuccessed) {
                var cate = result.ResultObj;
                var editVm = new CategoryUpdateRequest()
                {
                    ID_Category = cate.ID_Catetory,
                    Name_Category = cate.Name_Catetory,
                };
                return View(editVm);
            }
            return RedirectToAction("Error", "Home");
        }

        //cập nhật xong -post lên
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _categoryApiClient.UpdateCategory(request);
            if (result)
            {
                TempData["result"] = "Cập nhật ngành hàng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật ngành hàng thất bại");
            return View(request);
        }

    }
}
