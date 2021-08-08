using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.WebPOS.Controllers
{
    [Authorize]
    public class BaseHomeController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessions = context.HttpContext.Session.GetString("Token"); //lấy session
            if (sessions == null)
            {
                context.Result = new RedirectToActionResult("Index", "Account", null);
            }

            base.OnActionExecuting(context);
        }
    }
}