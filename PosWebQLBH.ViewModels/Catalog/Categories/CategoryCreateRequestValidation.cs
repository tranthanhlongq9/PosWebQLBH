using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Categories
{
    public class CategoryCreateRequestValidation : AbstractValidator<CategoryCreateRequest>
    {
        public CategoryCreateRequestValidation()
        {
            RuleFor(x => x.ID_Category).NotEmpty().WithMessage("Mã ngành hàng là bắt buộc")
                .Matches(@"^[a-zA-z0-9-]*$")
                .WithMessage("Mã ngành hàng không được sử dụng ký tự đặc biệt, khoảng trắng")
                .MaximumLength(30).WithMessage("Tên ngành hàng không được vượt quá 30 ký tự !!");

            RuleFor(x => x.Name_Category).NotEmpty().WithMessage("Tên ngành hàng là bắt buộc")
                .Matches(@"^[a-zA-z0-9 áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄóòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ]*$")
                .WithMessage("Tên ngành hàng không được sử dụng ký tự đặc biệt")
                .MaximumLength(200).WithMessage("Tên ngành hàng không được vượt quá 200 ký tự !!");
        }
    }
}