using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Categories
{
    public class CategoryVm
    {
        [Display(Name = "Mã loại:")]
        public string ID_Catetory { get; set; }

        [Display(Name = "Tên loại:")]
        public string Name_Catetory { get; set; }

        [Display(Name = "Tạo bởi:")]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày tạo:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Cập nhật bởi:")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Ngày cập nhật:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}")]
        public DateTime? UpdatedDate { get; set; }
    }
}
