using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosWebQLBH.WebPOS.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PosWebQLBH.WebPOS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //var product = await _productApiApp.GetAll();
            //return View(product);
            return View();
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