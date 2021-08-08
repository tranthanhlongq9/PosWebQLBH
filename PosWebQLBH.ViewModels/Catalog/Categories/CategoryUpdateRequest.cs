using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Categories
{
    public class CategoryUpdateRequest
    {
        [Display(Name = "Mã ngành hàng:")]
        public string ID_Category { get; set; }

        [Display(Name = "Tên ngành hàng:")]
        public string Name_Category { get; set; }

        [Display(Name = "Tạo bởi:")]
        public string CreatedBy { get; set; }

        [Display(Name = "Cập nhật bởi:")]
        public string UpdatedBy { get; set; }

    }
}
