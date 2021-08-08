using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.Partner.Customers;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PosWebQLBH.AdminApp.Services
{
    public class CustomerApiClient : BaseApiClient, ICustomerApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CustomerApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                                    IHttpContextAccessor httpContextAccessor)
                    : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        //Tạo 
        public async Task<bool> CreateCustomer(CustomerCreateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext
                                                .Session.GetString(SystemConstants.AppSettings.Token);

            //lấy language sau này cần
            //var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            //var userName =

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            requestContent.Add(new StringContent(request.ID_Customer.ToString()), "iD_Customer");
            requestContent.Add(new StringContent(request.Name_Customer.ToString()), "name_Customer");
            requestContent.Add(new StringContent(request.Phone_Number.ToString()), "phone_Number");
            requestContent.Add(new StringContent(request.Address.ToString()), "Address");
            requestContent.Add(new StringContent(request.CreatedBy.ToString()), "createdBy");

            var response = await client.PostAsync($"/api/customers/", requestContent);
            return response.IsSuccessStatusCode;

        }

        //show và phân trang
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

        ///lấy id kh
        public async Task<ApiResult<CustomerVm>> GetCustomerById(long customerId)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            //truyền token đăng nhập vào ủy quyền(Authorization)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/customers/{customerId}");

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<CustomerVm>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<CustomerVm>>(body);

        }

        //cập nhật
        public async Task<bool> UpdateCustomer(CustomerUpdateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext
                                                .Session.GetString(SystemConstants.AppSettings.Token);

            //var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();


            requestContent.Add(new StringContent(request.ID.ToString()), "iD_Customer");
            requestContent.Add(new StringContent(request.Name_Customer.ToString()), "name_Customer");
            requestContent.Add(new StringContent(request.Address.ToString()), "address");
            requestContent.Add(new StringContent(request.Phone_Number.ToString()), "phone");
            requestContent.Add(new StringContent(request.UpdatedBy.ToString()), "updatedBy");

            var response = await client.PutAsync($"/api/customers/" + request.ID, requestContent);
            return response.IsSuccessStatusCode;

        }

        //xóa customer
        public async Task<ApiResult<bool>> DeleteCustomer(long customerId)
        {
            //lấy session
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            //truyền token session đăng nhập vào ủy quyền(Authorization)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/customers/{customerId}");

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }
    }
}
