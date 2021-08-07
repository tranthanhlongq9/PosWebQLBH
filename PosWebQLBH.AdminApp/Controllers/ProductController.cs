using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
            _configuration = configuration;
        }

        //lấy product show lên
        public async Task<IActionResult> Index(string keyword, string categoryId, int pageIndex = 1, int pageSize = 10) //phân trang
        {
            //lấy session
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetManageProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
                CategoryId = categoryId
            };
            var data = await _productApiClient.GetProductPagings(request);
            ViewBag.Keyword = keyword; //để giữ dữ liệu keyword lại trên view khi tìm kiếm

            var categories = await _categoryApiClient.GetAll();
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.NameCate,
                Value = x.IdCate.ToString(), //nếu là kiểu int thì sẽ chuyển sang string
                Selected = categoryId == x.IdCate //gán giá trị vào view dropdown
            });

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMess = TempData["result"];
            }
            return View(data);
        }

        //Tạo product -- lấy form nhập
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryApiClient.GetAll();
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.IdCate + ": " + x.NameCate,
                Value = x.IdCate, //nếu là kiểu int thì sẽ chuyển sang string
            });

            return View();
        }

        //Tạo product-- tạo xong post lên
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(string categoryId, [FromForm] ProductCreateRequest request)
        {
            //dùng để lấy lên category đưa vào dropdown
            var categories = await _categoryApiClient.GetAll();
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.IdCate + ": " + x.NameCate,
                Value = x.IdCate, //nếu là kiểu int thì sẽ chuyển sang string
                Selected = categoryId == x.IdCate //gán giá trị vào view dropdown
            });
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công !!";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }

        //Cập nhật product -- lấy form dữ liệu
        [HttpGet]
        public async Task<IActionResult> Edit(string productId)
        {
            var result = await _productApiClient.GetProductById(productId);

            if (result.IsSuccessed)
            {
                var product = result.ResultObj;
                var editVm = new ProductUpdateRequest()
                {
                    ID_Product = product.ID,
                    ID_Category = product.ID_Category,
                    Name_Product = product.Name_Product,
                    Price = product.Price,
                    ID_Unit = product.ID_Unit,
                    Length = product.Length,
                    Width = product.Width,
                    Weight = product.Weight,
                    Height = product.Height,
                    Quantity = product.Quantity
                };
                return View(editVm);
            }
            return RedirectToAction("Error", "Home");
        }

        //Cập nhật product-- tạo xong post lên
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.UpdateProduct(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công !!";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(request);
        }

        //Xóa sản phẩm
        [HttpGet]
        public async Task<IActionResult> Delete(string productId)
        {
            var result = await _productApiClient.GetProductById(productId);
            if (result.IsSuccessed)
            {
                var infoProduct = result.ResultObj;
                return View(new ProductDeleteRequest()
                {
                    ID_Product = productId,
                    Name_Product = infoProduct.Name_Product
                });
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.DeleteProduct(request.ID_Product);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa sản phẩm Thành Công";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", result.Message); //sẽ thông báo lỗi có message lỗi
            return View(request);
        }

        //xem chi tiết
        [HttpGet]
        public async Task<IActionResult> Details(string productId)
        {
            var result = await _productApiClient.GetProductById(productId);
            return View(result.ResultObj);
        }
    }
}