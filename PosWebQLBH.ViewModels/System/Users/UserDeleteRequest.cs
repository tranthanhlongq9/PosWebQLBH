using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.System.Users
{
    public class UserDeleteRequest
    {
        public Guid Id { get; set; }

        [Display(Name = "Tài khoản - Mã nhân viên:")]
        public string UserName { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}