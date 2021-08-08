using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Units
{
    public class UnitUpdateRequestValidation : AbstractValidator<UnitUpdateRequest>
    {
        public UnitUpdateRequestValidation()
        {
            RuleFor(x => x.Name_Unit).NotEmpty().WithMessage("Tên ngành hàng là bắt buộc")
               .Matches(@"^[a-zA-z0-9 áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄóòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ]*$")
               .WithMessage("Tên ngành hàng không được sử dụng ký tự đặc biệt")
               .MaximumLength(200).WithMessage("Tên ngành hàng không được vượt quá 200 ký tự !!");
        }
    }
}
