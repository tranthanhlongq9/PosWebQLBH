using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Suppliers
{
    public class SupplierCreateRequestValidation : AbstractValidator<SupplierCreateRequest>
    {
        public SupplierCreateRequestValidation()
        {
            RuleFor(x => x.ID_Supplier).NotEmpty().WithMessage("Mã nhà cung cấp là bắt buộc")
                .Matches(@"^[a-zA-z0-9]*$")
                .WithMessage("Mã nhà cung cấp không được sử dụng ký tự đặc biệt, khoảng trắng")
                .MaximumLength(30).WithMessage("Mã nhà cung cấp không được vượt quá 30 ký tự !!");

            RuleFor(x => x.Name_Supplier).NotEmpty().WithMessage("Tên nhà cung cấp là bắt buộc")
                .Matches(@"^[a-zA-z0-9 áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄóòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ]*$")
                .WithMessage("Tên nhà cung cấp không được sử dụng ký tự đặc biệt")
                .MaximumLength(200).WithMessage("Tên nhà cung cấp không được vượt quá 200 ký tự !!");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Địa chỉ nhà cung cấp là bắt buộc")
               .Matches(@"^[a-zA-z0-9,. áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄóòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ]*$")
               .WithMessage("Địa chỉ nhà cung cấp không được sử dụng ký tự đặc biệt")
               .MaximumLength(500).WithMessage("Địa chỉ nhà cung cấp không được vượt quá 500 ký tự !!");

            RuleFor(x => x.Representative).NotEmpty().WithMessage("Tên người đại diện là bắt buộc")
                .Matches(@"^[a-zA-z0-9 áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄóòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ]*$")
                .WithMessage("Tên người đại diện không được sử dụng ký tự đặc biệt")
                .MaximumLength(200).WithMessage("Tên người đại diện không được vượt quá 200 ký tự !!");

            RuleFor(x => x.Phone_Number).NotEmpty().WithMessage("Số điện thoại là bắt buộc")
                .Matches(@"^[0-9]*$")
                .WithMessage("Số điện thoại phải là số")
                .MaximumLength(11).WithMessage("Số điện thoại phải có đủ 9 đến 11 số")
                .MinimumLength(9).WithMessage("Số điện thoại phải có đủ 9 đến 11 số");


        }
    }
}
