using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.ViewModels.Catalog.Categories;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<List<CategoryVm>> GetAll()
        {
            return await GetListAsync<CategoryVm>("/api/categories");
        }
    }
}