using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Suppliers
{
    public class SupplierDeleteRequest
    {
        public string ID_Supplier { get; set; }

        public string Name_Supplier { get; set; }

        public string Representative { get; set; }

        public string Phone_Number { get; set; }

        public string Address { get; set; }
    }
}
