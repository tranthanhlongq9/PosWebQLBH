using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.Partner.Customers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public class CustomerApiClient : BaseApiClient, ICustomerApiClient
    {
        public CustomerApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                                    IHttpContextAccessor httpContextAccessor)
                    : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<PagedResult<CustomerVm>> GetCustomerPagings(GetManageCustomerPagingRequest request)
        {
            var data = await GetAsync<PagedResult<CustomerVm>>(
                                            $"/api/customers/paging?pageIndex={request.PageIndex}" +
                                            $"&pageSize={request.PageSize}&keyword={request.Keyword}" +
                                            $"&languageId={request.LanguageId}");

            return data;
        }

        public async Task<ApiResult<List<CustomerVm>>> GetAll()
        {
            return await GetAsync<ApiResult<List<CustomerVm>>>("/api/customers");
        }
    }
}
