using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Data.Entities
{
    public class AppUser : IdentityUser<Guid> // guild : kiểu duy nhất cho toàn hệ thống
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; } //ngày sinh

        //public List<Cart> Carts { get; set; }

        //public List<Order> Orders { get; set; }

        //public List<Transaction> Transactions { get; set; } //List<..> dùng để liên kết 2 chiều
    }
}