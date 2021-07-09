using System;
using System.Collections.Generic;

#nullable disable

namespace PosWebQLBH.Data.Entities
{
    public partial class History
    {
        public long IdHistory { get; set; }
        public long IdSellOrder { get; set; }
        public long IdCustomer { get; set; }
        public long IdPurchaseOrder { get; set; }
        public string IdProduct { get; set; }
        public string IdEmployee { get; set; }
        public string IdSuppliers { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string TotalAmount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Discount { get; set; }
        public string PriceAfterDiscount { get; set; }
        public bool Type { get; set; }
    }
}
