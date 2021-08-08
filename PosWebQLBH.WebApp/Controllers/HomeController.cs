using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ActionName("Home")] //do dính endpoint nên để tạm tên này
        public IActionResult Index()
        {
            //using (DbQLBHContext context = new DbQLBHContext())
            //{
            //    Category category = new Category();
            //    category.IdCategory = "H33";
            //    category.NameCategory = "Cate là 33";
            //    category.CreatedBy = "Long";
            //    category.CreatedDate = DateTime.Now;
            //    category.UpdatedBy = "long1";
            //    category.UpdatedDate = DateTime.Now;
            //    context.Categories.Add(category);
            //    context.SaveChanges();

            //}
            return Ok();
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