using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Customers
{
    public class GetPublicCustomerPagingRequest : PagingRequestBase
    {
        public long CustomerId { get; set; }

    }
}
