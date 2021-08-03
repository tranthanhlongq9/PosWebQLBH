using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Products
{
    public class ProductCreateValidation : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateValidation()
        {
            RuleFor(x => x.ID_Product).NotEmpty().WithMessage("Mã sản phẩm là bắt buộc")
                .MaximumLength(100).WithMessage("Mã sản phẩm không được vượt quá 100 ký tự !!");

            RuleFor(x => x.Name_Product).NotEmpty().WithMessage("Tên sản phẩm là bắt buộc")
                .MaximumLength(200).WithMessage("Tên sản phẩm không được vượt quá 200 ký tự !!");

            RuleFor(x => x.ID_Category).NotEmpty().WithMessage("Mã loại sản phẩm là bắt buộc")
                .MaximumLength(200).WithMessage("Mã loại sản phẩm không được vượt quá 200 ký tự !!");

            RuleFor(x => x.ID_Unit).NotEmpty().WithMessage("Mã đơn vị là bắt buộc")
                .MaximumLength(100).WithMessage("Tên sản phẩm không được vượt quá 100 ký tự !!");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Giá sản phẩm là bắt buộc");

            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Giá sản phẩm là bắt buộc");

            RuleFor(x => x.Length).NotEmpty().WithMessage("Chiều dài là bắt buộc");

            RuleFor(x => x.Width).NotEmpty().WithMessage("chiều rộng là bắt buộc");

            RuleFor(x => x.Height).NotEmpty().WithMessage("chiều cao là bắt buộc");

            RuleFor(x => x.Weight).NotEmpty().WithMessage("Cân nặng là bắt buộc");

            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("Tạo bởi ai là bắt buộc")
                .MaximumLength(100).WithMessage("Tạo bởi ai không được vượt quá 100 ký tự !!");

            RuleFor(x => x.ThumbnailImage).NotNull().WithMessage("Hình ảnh sản phẩm là bắt buộc");
        }
    }
}