using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.System.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            //RuleFor(x => x.UserName).NotEmpty().WithMessage("Tài khoản không được để trống");
            //RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống")
            //    .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự !");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Tài khoản không được để trống")
                .Matches(@"^[a-zA-z0-9@.]*$")
                .WithMessage("Tài khoản không được sử dụng ký tự đặc biệt, khoảng trắng");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự !")
                .MaximumLength(30).WithMessage("Mật khẩu không vượt quá 30 ký tự !")
                .Matches(@"^[a-zA-z0-9@]*$")
                .WithMessage("Mật khẩu không được sử dụng ký tự đặc biệt, khoảng trắng");
        }
    }
}