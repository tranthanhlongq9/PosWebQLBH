using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Customers
{
    public class CustomerValidation : AbstractValidator<CustomerCreateRequest>
    {
        public CustomerValidation()
        {
            RuleFor(x => x.ID_Customer).NotEmpty().WithMessage("Mã khách hàng là bắt buộc");
               

            RuleFor(x => x.Name_Customer).NotEmpty().WithMessage("Tên khách hàng là bắt buộc")
                .Matches(@"^[a-zA-z0-9]*$")
                .WithMessage("Tên khách hàng không được sử dụng ký tự đặc biệt, khoảng trắng")
                .MaximumLength(200).WithMessage("Tên khách hàng không được vượt quá 200 ký tự !!");

            RuleFor(x => x.Phone_Number).NotEmpty().WithMessage("Số điện thoại là bắt buộc")
                .Matches(@"^[0-9]*$").WithMessage("Số điện thoại phải là số !!")
                .MinimumLength(9).WithMessage("Số điện thoại ít nhất 9 số !!")
                .MaximumLength(11).WithMessage("Số điện thoại nhiều nhất 11 số!!");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Địa chỉ là bắt buộc")
                .Matches(@"^[a-zA-z0-9 ]*$")
                .WithMessage("Địa chỉ không được sử dụng ký tự đặc biệt")
                .MaximumLength(200).WithMessage("Địa chỉ không được vượt quá 200 ký tự !!");

            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("Người tạo là bắt buộc")
                .MaximumLength(200).WithMessage("Người tạo không được vượt quá 200 ký tự !!");
        } 
    }
}
