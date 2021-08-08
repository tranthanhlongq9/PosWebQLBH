using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Catalog.Units
{
    public class UnitDeleteRequest
    {
        public string ID_Unit { get; set; }

        public string Name_Unit { get; set; } 
    }
}