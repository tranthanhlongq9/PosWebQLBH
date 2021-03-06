using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Customers
{
    public class CustomerVm
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

        [Display(Name = "Ngày tạo:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}")] // ( ,ApplyFormatInEditMode = true)
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Cập nhật bởi:")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Ngày cập nhật:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}")]
        public DateTime? UpdatedDate { get; set; }

        //[Display(Name = "Mã hóa đơn:")]
        //public long ID_Sell { get; set; }
        
    }
}
