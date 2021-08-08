using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosWebQLBH.WebPOS.Models;
using PosWebQLBH.WebPOS.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.WebPOS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApiApp _productApiApp;

        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductApiApp productApiApp, ILogger<HomeController> logger)
        {
            _logger = logger;
            _productApiApp = productApiApp;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
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