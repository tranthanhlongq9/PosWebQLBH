using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.WebPOS.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.WebPOS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        private readonly ICategoryApiClient _categoryApiClient;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ICategoryApiClient categoryApiClient, IProductApiClient productApiClient, ILogger<HomeController> logger)
        {
            _logger = logger;
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index() //string categoryId
        {
            var result = await _productApiClient.GetAllPro();
            var model = JsonConvert.DeserializeObject<List<ProductViewModel>>(result);

            //var categories = await _categoryApiClient.GetAll();
            //ViewBag.Categories = categories.Select(x => new SelectListItem()
            //{
            //    Text = x.Name_Catetory,
            //    Value = x.ID_Catetory.ToString(), //nếu là kiểu int thì sẽ chuyển sang string
            //    Selected = categoryId == x.ID_Catetory //gán giá trị vào view dropdown
            //});

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}