using eShopSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.Utilities.Exceptions;
using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.System.Users
{
    public class UserService : IUserService
    {
        //login

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                            RoleManager<AppRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        //hàm xử lý đăng nhập
        public async Task<string> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                //Claim: yêu cầu để lưu vào
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            //để tạo ra token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //hàm lấy user và sắp xếp show theo trang
        public async Task<PagedResult<UserVm>> GetUserPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                    || x.PhoneNumber.Contains(request.Keyword) || x.Email.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVm()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    Birthday = x.Dob
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserVm>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        //hàm tạo tài khoản
        public async Task<bool> Register(RegisterRequest request)
        {
            //var user = new Employee()
            //{
            //    IdEmployee = request.UserName,
            //    NameEmployee = request.NameEmployee,
            //    PhoneNumber = request.PhoneNumber,
            //    Gender = request.Gender,
            //    Birthday = request.Birthday,
            //    Email = request.Email,
            //    CreatedBy = request.CreatedBy,
            //    CreatedDate = DateTime.Now,
            //};

            var user = new AppUser()
            {
                Dob = request.Birthday,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}