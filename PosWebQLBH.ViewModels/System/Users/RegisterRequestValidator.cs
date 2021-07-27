using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.System.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator() //hàm bắt lỗi khi nhập vào
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name là bắt buộc")
                .MaximumLength(200).WithMessage("First name không được vượt quá 200 ký tự !!");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name là bắt buộc")
                .MaximumLength(200).WithMessage("Last Name không được vượt quá 200 ký tự !!");

            RuleFor(x => x.Birthday).NotEmpty().WithMessage("Ngày sinh không được để trống")
                .GreaterThan(DateTime.Now.AddYears(-100))
                .WithMessage("Ngày tháng năm sinh không được vượt quá 100 năm !!");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email là bắt buộc")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") //email pattern
                .WithMessage("Định dạng Email không khớp - không được sử dụng những ký tự đặc biệt");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại là bắt buộc")
                .MaximumLength(11).WithMessage("SĐT phải có đủ 10 đến 11 số")
                .MinimumLength(10).WithMessage("SĐT phải có đủ 10 đến 11 số");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name là bắt buộc");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password là bắt buộc")
                .MinimumLength(6).WithMessage("Password phải có ít nhất 6 ký tự !");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password nhập vào không khớp");
                }
            });
        }
    }
}