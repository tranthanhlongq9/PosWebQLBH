using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Categories
{
    public class CategoryUpdateRequestValidation : AbstractValidator<CategoryUpdateRequest>
    {
        public CategoryUpdateRequestValidation(){

            RuleFor(x => x.Name_Category).NotEmpty().WithMessage("Tên ngành hàng là bắt buộc")
                .Matches(@"^[a-zA-z0-9 áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄóòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ]*$")
                .WithMessage("Tên ngành hàng không được sử dụng ký tự đặc biệt")
                .MaximumLength(200).WithMessage("Tên ngành hàng không được vượt quá 200 ký tự !!");

        }

    }
}
