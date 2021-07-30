using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.System.Users
{
    public class UserVm
    {
        public Guid Id { get; set; }

        [Display(Name = "Tên:")]
        public string FirstName { get; set; }

        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        [Display(Name = "Ngày sinh:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}")] // ( ,ApplyFormatInEditMode = true)
        public DateTime Birthday { get; set; }

        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tài khoản ( Mã nhân viên ):")]
        public string UserName { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}