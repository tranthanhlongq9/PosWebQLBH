using System;
using System.Collections.Generic;

#nullable disable

namespace PosWebQLBH.Data.Entities
{
    public partial class Unit
    {
        public Unit()
        {
            Products = new HashSet<Product>();
        }

        public string IdUnit { get; set; }
        public string NameUnit { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
