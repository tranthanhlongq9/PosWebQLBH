using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.ViewModels.Partner.Suppliers
{
    public class GetSupplierPagingRequest : PagingRequestBase 
    {
        public string Keyword { get; set; }

        public string LanguageId { get; set; }
    }
}
