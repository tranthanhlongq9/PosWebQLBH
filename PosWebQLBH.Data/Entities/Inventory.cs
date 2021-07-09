using System;
using System.Collections.Generic;

#nullable disable

namespace PosWebQLBH.Data.Entities
{
    public partial class Inventory
    {
        public long IdInventory { get; set; }
        public string IdProduct { get; set; }
        public int Quantity { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Product IdProductNavigation { get; set; }
    }
}
