using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Products
{
    public class ProductViewModel
    {
        [Display(Name = "Mã sản phẩm:")]
        public string ID { get; set; }

        [Display(Name = "Mã loại:")]
        public string ID_Category { get; set; }

        [Display(Name = "Tên loại:")]
        public string Name_Category { get; set; }

        [Display(Name = "Tên sản phẩm:")]
        public string Name_Product { get; set; }

        [Display(Name = "Giá:")]
        [DisplayFormat(DataFormatString = "{0:0,0 VNĐ}")] //định dạng kiểu tiền vnd -- ( DataFormatString = "{0:C}") này để dịnh dạng kiểu tiền $ )
        public decimal Price { get; set; }

        [Display(Name = "Mã đơn vị:")]
        public string ID_Unit { get; set; }

        [Display(Name = "Đơn vị:")]
        public string Name_Unit { get; set; }

        [Display(Name = "Chiều dài:")]
        public decimal? Length { get; set; }

        [Display(Name = "chiều rộng:")]
        public decimal? Width { get; set; }

        [Display(Name = "chiều cao:")]
        public decimal? Height { get; set; }

        [Display(Name = "Cân nặng:")]
        public decimal? Weight { get; set; }

        [Display(Name = "Tạo bởi:")]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày tạo:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}")] // ( ,ApplyFormatInEditMode = true)
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Cập nhật bởi:")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Ngày cập nhật:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Số lượng:")]
        public int Quantity { get; set; }

        public string ThumbnailImage { get; set; }
    }
}