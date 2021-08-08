using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Suppliers
{
    public class SupplierValidation : AbstractValidator<SupplierCreateRequest>
    {
        public SupplierValidation()
        {
            RuleFor(x => x.ID_Supplier).NotEmpty().WithMessage("Mã nhà cung cấp là bắt buộc")
                 .Matches(@"^[a-zA-z0-9]*$")
                .WithMessage("Mã nhà cung cấp không được sử dụng ký tự đặc biệt, khoảng trắng");

            RuleFor(x => x.Name_Supplier).NotEmpty().WithMessage("Tên nhà cung cấp là bắt buộc")
                .Matches(@"^[a-zA-z0-9]*$")
                .WithMessage("Tên nhà cung cấp không được sử dụng ký tự đặc biệt, khoảng trắng")
                .MaximumLength(200).WithMessage("Tên nhà cung cấp không được vượt quá 200 ký tự !!");

            RuleFor(x => x.Phone_Number).NotEmpty().WithMessage("Số điện thoại là bắt buộc")
                .Matches(@"^[0-9]*$").WithMessage("Số điện thoại phải là số !!")
                .MinimumLength(9).WithMessage("Số điện thoại ít nhất 9 số !!")
                .MaximumLength(11).WithMessage("Số điện thoại nhiều nhất 11 số!!");

            RuleFor(x => x.Representative).NotEmpty().WithMessage("Người liên hệ là bắt buộc")
                .Matches(@"^[a-zA-z0-9 ]*$")
                .WithMessage("Người liên hệ không được sử dụng ký tự đặc biệt")
                .MaximumLength(200).WithMessage("Người liên hệ không được vượt quá 200 ký tự !!");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Địa chỉ là bắt buộc")
                .Matches(@"^[a-zA-z0-9 ]*$")
                .WithMessage("Địa chỉ không được sử dụng ký tự đặc biệt")
                .MaximumLength(200).WithMessage("Địa chỉ không được vượt quá 200 ký tự !!");

            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("Người tạo là bắt buộc")
                .MaximumLength(200).WithMessage("Người tạo không được vượt quá 200 ký tự !!");

        }
    }
}
