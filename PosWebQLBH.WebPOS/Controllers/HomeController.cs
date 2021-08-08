using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.WebPOS.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PosWebQLBH.WebPOS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductApiClient productApiClient, ILogger<HomeController> logger)
        {
            _logger = logger;
            _productApiClient = productApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _productApiClient.GetAll();
            return View(result);

            //if (result.IsSuccessed)
            //{
            //    var product = result.ResultObj;
            //    var editVm = new SelectListItem()
            //    {
            //        ID = product.,
            //        ID_Category = product.ID_Category,
            //        Name_Product = product.Name_Product,
            //        Name_Category = product.Name_Category,
            //        Price = product.Price,
            //        ID_Unit = product.ID_Unit,
            //        Name_Unit = product.Name_Unit,
            //        Length = product.Length,
            //        Width = product.Width,
            //        Weight = product.Weight,
            //        Height = product.Height,
            //        Quantity = product.Quantity
            //    };
            //    return View(editVm);
            //}
            //return RedirectToAction("Error", "Home");
            //return View();
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