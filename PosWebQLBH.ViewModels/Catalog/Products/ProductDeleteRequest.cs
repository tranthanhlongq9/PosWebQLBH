using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Products
{
    public class ProductDeleteRequest
    {
        [Display(Name = "Mã sản phẩm: ")]
        public string ID_Product { get; set; }

        [Display(Name = "Tên sản phẩm: ")]
        public string Name_Product { get; set; }
    }
}