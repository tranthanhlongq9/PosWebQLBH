using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Customers
{
    public class CustomerUpdateRequest
    {
        [Display(Name = "Mã khách hàng:")]
        public long ID { get; set; }

        [Display(Name = "Tên khách hàng:")]
        public string Name_Customer { get; set; }

        [Display(Name = "Số điện thoại:")]
        public string Phone_Number { get; set; }

        [Display(Name = "Địa chỉ:")]
        public string Address { get; set; }

        [Display(Name = "Người tạo:")]
        public string CreatedBy { get; set; }

        [Display(Name = "Cập nhật bởi:")]
        public string UpdatedBy { get; set; }

        
    }
}
