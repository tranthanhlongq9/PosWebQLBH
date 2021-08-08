using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.ViewModels.Catalog.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public class UnitApiClient : BaseApiClient, IUnitApiClient
    {
        public UnitApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<List<UnitVmodel>> GetAll()
        {
            return await GetListAsync<UnitVmodel>("/api/units");
        }
    }
}