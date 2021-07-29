using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.System.Users
{
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Tên là bắt buộc")
                .MaximumLength(200).WithMessage("First name không được vượt quá 200 ký tự !!");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Họ là bắt buộc")
                .MaximumLength(200).WithMessage("Last Name không được vượt quá 200 ký tự !!");

            RuleFor(x => x.Birthday).NotEmpty().WithMessage("Ngày sinh không được để trống")
                .GreaterThan(DateTime.Now.AddYears(-100))
                .WithMessage("Ngày tháng năm sinh không được vượt quá 100 năm !!");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại là bắt buộc")
                    .MaximumLength(11).WithMessage("SĐT phải có đủ 10 đến 11 số")
                    .MinimumLength(10).WithMessage("SĐT phải có đủ 10 đến 11 số");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email là bắt buộc")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") //email pattern
                .WithMessage("Định dạng Email không khớp - Không được sử dụng những ký tự đặc biệt");
        }
    }
}