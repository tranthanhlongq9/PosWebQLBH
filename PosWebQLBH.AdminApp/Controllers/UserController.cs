using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.System.Users;
using System;
using System.Text;
using System.Threading.Tasks;

//adminApp sẽ tương tác bên ngoài
namespace PosWebQLBH.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IRoleApiClient _roleApiClient;

        public UserController(IUserApiClient userApiClient, IConfiguration configuration,
                              IRoleApiClient roleApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _roleApiClient = roleApiClient;
        }

        //phương thức lấy user
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10) //phân trang
        {
            //var sessions = HttpContext.Session.GetString("Token"); //lấy session
            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUsersPagings(request);
            ViewBag.Keyword = keyword; //để giữ dữ liệu keyword lại trên view khi tìm kiếm

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMess = TempData["result"];
            }
            return View(data.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RegisterUser(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Tạo Người Dùng Thành Công !!";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", result.Message); //sẽ thông báo lỗi có message lỗi
            return View(request);
        }

        [HttpGet] //lấy dữ liệu
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userApiClient.GetUserById(id);

            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateRequest()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Birthday = user.Birthday,
                    Id = id
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost] //đưa dữ liệu vào db
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid) // ModelState: lỗi hệ thống
                return View();

            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Chỉnh sửa người dùng thành công.. ";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", result.Message); //sẽ thông báo lỗi có message lỗi (lỗi model)
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetUserById(id);
            return View(result.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //đăng xuất SignOutAsync -- xóa cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("Token"); //xóa token session đc cấp lúc đăng nhập

            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(new UserDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.DeleteUser(request.Id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa Người Dùng Thành Công";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", result.Message); //sẽ thông báo lỗi có message lỗi
            return View(request);
        }

        [HttpGet] //lấy dữ liệu
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }

        [HttpPost] //đưa dữ liệu thay đổi vào db
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid) // ModelState: lỗi hệ thống
                return View();

            var result = await _userApiClient.RoleAssign(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền cho người dùng thành công ";
                return RedirectToAction("Index"); //nếu thành công thì chuyển đến index ở trên
            }

            ModelState.AddModelError("", result.Message); //sẽ thông báo lỗi có message lỗi (lỗi model)
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);

            return View(roleAssignRequest);
        }

        //lấy tt Role show lên
        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userObj = await _userApiClient.GetUserById(id);
            var roleObj = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roleObj.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userObj.ResultObj.Roles.Contains(role.Name),
                    Description = role.Description
                });
            }

            return roleAssignRequest;
        }
    }
}