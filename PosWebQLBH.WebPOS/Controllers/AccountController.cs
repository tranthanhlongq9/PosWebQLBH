using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.WebPOS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public AccountController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //xử lý: cứ vào trang login là sẽ logout những session cũ đi
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid)
                //return View(ModelState); //bỏ cái này vì khi lỗi sẽ show ra modelState
                return View(); //dùng cái này có lỗi sẽ trả về view

            var result = await _userApiClient.Authenticate(request); //lấy token
            if (result.ResultObj == null) //nếu ko có tài khoản thì sẽ trả về message lỗi
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }

            var userPrincipal = this.ValidateToken(result.ResultObj);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10), //sau 10p sẽ out session
                IsPersistent = false
            };

            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId,
                                            _configuration[SystemConstants.AppSettings.DefaultLanguageId]);
            HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
            //Cookie và đăng nhập
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);

            //ModelState.AddModelError("", result.Message);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //đăng xuất SignOutAsync -- xóa cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("Token"); //xóa token session đc cấp lúc đăng nhập

            return RedirectToAction("Index", "Account");
        }

        //hàm xử lý token
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
    }
}