using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.WebPOS.Services
{
    public interface IProductApiApp
    {
        Task<ApiResult<List<ProductViewModel>>> GetAll();
    }
}