using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Customers
{
    public class CustomerUpdateRequestValidation : AbstractValidator<CustomerUpdateRequest>
    {
        public CustomerUpdateRequestValidation()
        {

            RuleFor(x => x.Name_Customer).NotEmpty().WithMessage("Tên khách hàng là bắt buộc")
                .Matches(@"^[a-zA-z0-9 áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄóòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ]*$")

                .WithMessage("Tên khách hàng không được sử dụng ký tự đặc biệt")
                .MaximumLength(200).WithMessage("Tên khách hàng không được vượt quá 200 ký tự !!");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Địa chỉ khách hàng là bắt buộc")
               .Matches(@"^[a-zA-z0-9,. áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄóòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ]*$")
               .WithMessage("Địa chỉ khách hàng không được sử dụng ký tự đặc biệt")
               .MaximumLength(500).WithMessage("Địa chỉ khách hàng không được vượt quá 500 ký tự !!");

            RuleFor(x => x.Phone_Number).NotEmpty().WithMessage("Số điện thoại là bắt buộc")
                .Matches(@"^[0-9]*$")
                .WithMessage("Số điện thoại phải là số")
                .MaximumLength(11).WithMessage("Số điện thoại phải có đủ 9 đến 11 số")
                .MinimumLength(9).WithMessage("Số điện thoại phải có đủ 9 đến 11 số");
        }
    }
}