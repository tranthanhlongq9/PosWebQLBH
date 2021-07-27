using System;
using System.Collections.Generic;

#nullable disable

namespace PosWebQLBH.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            Inventories = new HashSet<Inventory>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SellOrders = new HashSet<SellOrder>();
        }

        public string IdProduct { get; set; }
        public string IdUnit { get; set; }
        public string IdCategory { get; set; }
        public string NameProduct { get; set; }
        public decimal Price { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string ImagePath { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Unit IdUnitNavigation { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SellOrder> SellOrders { get; set; }
    }
}
