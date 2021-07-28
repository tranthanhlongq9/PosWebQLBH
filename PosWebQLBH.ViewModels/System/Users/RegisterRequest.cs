using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.System.Users
{
    public class RegisterRequest
    {
        [Display(Name = "Tài khoản - Mã nhân viên")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu:")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Tên:")]
        public string FirstName { get; set; }

        [Display(Name = "Họ:")]
        public string LastName { get; set; }

        //public string NameEmployee { get; set; }

        //public bool Gender { get; set; }

        [Display(Name = "Ngày sinh:")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại:")]
        public string PhoneNumber { get; set; }

        //public string CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string UpdatedBy { get; set; }
        //public DateTime? UpdatedDate { get; set; }
    }
}