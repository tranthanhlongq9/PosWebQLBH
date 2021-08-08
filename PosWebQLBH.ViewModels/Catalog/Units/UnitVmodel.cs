using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Units
{
    public class UnitVmodel
    {
        [Display(Name = "Mã đơn vị:")]
        public string IdUnit { get; set; }

        [Display(Name = "Tên đơn vị:")]
        public string NameUnit { get; set; }
    }
}