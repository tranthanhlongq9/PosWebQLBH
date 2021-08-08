using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Customers
{
    public class CustomerDeleteRequest
    {
        public long ID { get; set; }

        public string Name_Customer { get; set; }

        public string Phone_Number { get; set; }

        public string Address { get; set; }
    }
}
