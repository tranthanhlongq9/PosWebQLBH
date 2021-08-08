using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PosWebQLBH.WebPOS.Services
{
    public class ProductApiApp : BaseApiClient, IProductApiApp
    {
        public ProductApiApp(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<ApiResult<List<ProductViewModel>>> GetAll()
        {
            return await GetAsync<ApiResult<List<ProductViewModel>>>("/api/products");
        }
    }
}