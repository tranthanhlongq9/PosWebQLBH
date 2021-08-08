using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.Catalog.Units;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public class UnitApiClient : BaseApiClient, IUnitApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UnitApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                                    IHttpContextAccessor httpContextAccessor)
                    : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        //show và phân trang
        public async Task<PagedResult<UnitVm>> GetUnitPagings(GetUnitPagingRequest request)
        {
            var data = await GetAsync<PagedResult<UnitVm>>(
                                             $"/api/units/paging?pageIndex={request.PageIndex}" +
                                             $"&pageSize={request.PageSize}&keyword={request.Keyword}" +
                                             $"&languageId={request.LanguageId}");

            return data;
        }

        //Tạo 
        public async Task<bool> CreateUnit(UnitCreateRequest request)
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

            requestContent.Add(new StringContent(request.ID_Unit.ToString()), "iD_Unit");
            requestContent.Add(new StringContent(request.Name_Unit.ToString()), "name_Unit");
            requestContent.Add(new StringContent(request.CreatedBy.ToString()), "createdBy");
            //requestContent.Add(new StringContent(request.UpdatedBy.ToString()), "updatedBy");

            var response = await client.PostAsync($"/api/units/", requestContent);
            return response.IsSuccessStatusCode;
        }

        //show all
        public async Task<ApiResult<List<UnitVm>>> GetAll()
        {
            return await GetAsync<ApiResult<List<UnitVm>>>("/api/units");
        }


        //cập nhật
        public async Task<bool> UpdateUnit(UnitUpdateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext
                                                .Session.GetString(SystemConstants.AppSettings.Token);

            //var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();


            requestContent.Add(new StringContent(request.ID_Unit.ToString()), "iD_Unit");
            requestContent.Add(new StringContent(request.Name_Unit.ToString()), "name_Unit");
            requestContent.Add(new StringContent(request.UpdatedBy.ToString()), "updatedBy");

            var response = await client.PutAsync($"/api/units/" + request.ID_Unit, requestContent);
            return response.IsSuccessStatusCode;

        }

        ///
        public async Task<ApiResult<UnitVm>> GetUnitById(string unitId)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            //truyền token đăng nhập vào ủy quyền(Authorization)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/units/{unitId}");

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<UnitVm>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<UnitVm>>(body);

        }

        //xóa unit
        public async Task<ApiResult<bool>> DeleteUnit(string unitId)
        {
            //lấy session
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            //truyền token session đăng nhập vào ủy quyền(Authorization)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/units/{unitId}");

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }
    }
}
