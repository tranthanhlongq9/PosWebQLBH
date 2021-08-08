using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Units
{
    public class UnitCreateRequestValidation : AbstractValidator<UnitCreateRequest>
    {
        public UnitCreateRequestValidation()
        {
            RuleFor(x => x.ID_Unit).NotEmpty().WithMessage("Mã đơn vị tính là bắt buộc")
                .Matches(@"^[a-zA-z0-9-]*$")
                .WithMessage("Mã đơn vị tính không được sử dụng ký tự đặc biệt, khoảng trắng")
                .MaximumLength(30).WithMessage("Mã đơn vị tính không được vượt quá 200 ký tự !!");

            RuleFor(x => x.Name_Unit).NotEmpty().WithMessage("Tên đơn vị tính là bắt buộc")
                .Matches(@"^[a-zA-z0-9 áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄóòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ]*$")
                .WithMessage("Tên đơn vị tính không được sử dụng ký tự đặc biệt")
                .MaximumLength(200).WithMessage("Tên đơn vị tính không được vượt quá 200 ký tự !!");
        }
    }
}