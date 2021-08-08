using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.Catalog.Categories;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CategoryApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                                    IHttpContextAccessor httpContextAccessor)
                    : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        //show và phân trang
        public async Task<PagedResult<CategoryVm>> GetCategoryPagings(GetCategoryPagingRequest request)
        {
            var data = await GetAsync<PagedResult<CategoryVm>>(
                                             $"/api/categories/paging?pageIndex={request.PageIndex}" +
                                             $"&pageSize={request.PageSize}&keyword={request.Keyword}" +
                                             $"&languageId={request.LanguageId}");

            return data;
        }

        //Tạo 
        public async Task<bool> CreateCategory(CategoryCreateRequest request)
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

            requestContent.Add(new StringContent(request.ID_Category.ToString()), "iD_Category");
            requestContent.Add(new StringContent(request.Name_Category.ToString()), "name_Category");
            requestContent.Add(new StringContent(request.CreatedBy.ToString()), "createdBy");
            //requestContent.Add(new StringContent(request.UpdatedBy.ToString()), "updatedBy");

            var response = await client.PostAsync($"/api/categories/", requestContent);
            return response.IsSuccessStatusCode;
        }

        //show all
        public async Task<ApiResult<List<CategoryVm>>> GetAll()
        {
            return await GetAsync<ApiResult<List<CategoryVm>>>("/api/categories");
        }

        //xóa
        //public async Task<bool> DeleteCategory(string categoryid)
        public async Task<ApiResult<bool>> DeleteCategory(string categoryId)
        {
            //lấy session
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            //truyền token session đăng nhập vào ủy quyền(Authorization)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/categories/{categoryId}");

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }

        //cập nhật
        public async Task<bool> UpdateCategory(CategoryUpdateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext
                                                .Session.GetString(SystemConstants.AppSettings.Token);

            //var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();


            requestContent.Add(new StringContent(request.ID_Category.ToString()), "iD_Category");
            requestContent.Add(new StringContent(request.Name_Category.ToString()), "name_Category");
            requestContent.Add(new StringContent(request.UpdatedBy.ToString()), "updatedBy");

            var response = await client.PutAsync($"/api/categories/" + request.ID_Category, requestContent);
            return response.IsSuccessStatusCode;

        }

        //lấy theo id
        public async Task<ApiResult<CategoryVm>> GetCategoryById(string categoryId)
        { 
            //lấy session
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            //truyền token đăng nhập vào ủy quyền(Authorization)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/categories/{categoryId}");

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<CategoryVm>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<CategoryVm>>(body);

        }
 
    }
}
