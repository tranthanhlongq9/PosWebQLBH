using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Customers
{
    public class CustomerCreateRequest
    {
        public long ID_Customer { get; set; }

        public string Name_Customer { get; set; }

        public string Address { get; set; }

        public string Phone_Number { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        //public long ID_SellOrder { get; set; }

    }
}
