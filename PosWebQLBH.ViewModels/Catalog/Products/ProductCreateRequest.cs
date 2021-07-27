using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        public string ID_Product { get; set; }

        public string ID_Category { get; set; }

        public string Name_Product { get; set; }

        public decimal Price { get; set; }

        public string ID_Unit { get; set; }

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public int Quantity { get; set; }

        public IFormFile ThumbnailImage { get; set; }


    }
}
