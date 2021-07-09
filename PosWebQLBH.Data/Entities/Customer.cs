using System;
using System.Collections.Generic;

#nullable disable

namespace PosWebQLBH.Data.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            SellOrders = new HashSet<SellOrder>();
        }

        public long IdCustomer { get; set; }
        public string NameCustomer { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<SellOrder> SellOrders { get; set; }
    }
}
