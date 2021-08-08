using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace PosWebQLBH.ViewModels.Catalog.Units
{
    public class UnitUpdateRequest
    {
        public string ID_Unit { get; set; }

        public string Name_Unit { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

    }
}
