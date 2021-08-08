using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Suppliers
{
    public class SupplierUpdateRequest
    {
        [Display(Name = "Mã nhà cung cấp:")]
        public string ID_Supplier { get; set; }

        [Display(Name = "Tên nhà cung cấp:")]
        public string Name_Supplier { get; set; }

        [Display(Name = "Điện thoại nhà cung cấp:")]
        public string Phone_Number { get; set; }

        [Display(Name = "Người liên hệ:")]
        public string Representative { get; set; }

        [Display(Name = "Địa chỉ nhà cung cấp:")]
        public string Address { get; set; }

        [Display(Name = "Người tạo:")]
        public string CreatedBy { get; set; }

        [Display(Name = "Cập nhật bởi:")]
        public string UpdatedBy { get; set; }
    }
}
