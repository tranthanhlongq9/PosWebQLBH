using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Products
{
    public class ProductUpdateRequest
    {
        [Display(Name = "Mã sản phẩm:")]
        public string ID_Product { get; set; }

        [Display(Name = "Mã sản phẩm mới:")]
        public string ID_ProductNew { get; set; }

        [Display(Name = "Mã loại:")]
        public string ID_Category { get; set; }

        [Display(Name = "Tên sản phẩm:")]
        public string Name_Product { get; set; }

        [Display(Name = "Giá:")]
        [DisplayFormat(DataFormatString = "{0:0,0 VNĐ}")]
        public decimal Price { get; set; }

        [Display(Name = "Mã đơn vị:")]
        public string ID_Unit { get; set; }

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

        [Display(Name = "Cập nhật bởi:")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Số lượng:")]
        public int Quantity { get; set; }

        [Display(Name = "Hình ảnh sản phẩm:")]
        public IFormFile ThumbnailImage { get; set; }
    }
}