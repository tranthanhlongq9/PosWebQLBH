using System;
using System.Collections.Generic;

#nullable disable

namespace PosWebQLBH.Data.Entities
{
    public partial class SellOrder
    {
        public long IdSellOrder { get; set; }
        public string IdEmployee { get; set; }
        public long IdCustomer { get; set; }
        public string IdProduct { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public decimal Discount { get; set; }
        public decimal PriceAfterDiscount { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; }
        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
