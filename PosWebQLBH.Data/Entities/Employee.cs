using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace PosWebQLBH.Data.Entities
{
    public partial class Employee /*: IdentityUser<Guid>*/
    {
        public Employee()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SellOrders = new HashSet<SellOrder>();
        }

        public string IdEmployee { get; set; }
        public string IdRole { get; set; }
        public string NameEmployee { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Role IdRoleNavigation { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SellOrder> SellOrders { get; set; }
    }
}