using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Categories
{
    public class CategoryCreateRequest
    {
        [Display(Name = "Mã ngành hàng:")]
        public string ID_Category { get; set; }

        [Display(Name = "Tên ngành hàng:")]
        public string Name_Category { get; set; }

        [Display(Name = "Người tạo:")]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày tạo:")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Cập nhật bởi:")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Ngày cập nhật :")]
        public DateTime? UpdatedDate { get; set; }
    }
}
