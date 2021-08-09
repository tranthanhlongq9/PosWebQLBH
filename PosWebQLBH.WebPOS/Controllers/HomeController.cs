using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.WebPOS.Models;
using System.Collections.Generic;
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
            var result = await _productApiClient.GetAllPro();
            var model = JsonConvert.DeserializeObject<List<ProductViewModel>>(result);

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