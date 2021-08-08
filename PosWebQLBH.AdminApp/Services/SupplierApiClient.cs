using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.Partner.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public class SupplierApiClient : BaseApiClient, ISupplierApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public SupplierApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                                    IHttpContextAccessor httpContextAccessor)
                    : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        //show và phân trang
        public async Task<PagedResult<SupplierVm>> GetSupplierPagings(GetSupplierPagingRequest request)
        {
            var data = await GetAsync<PagedResult<SupplierVm>>(
                                             $"/api/suppliers/paging?pageIndex={request.PageIndex}" +
                                             $"&pageSize={request.PageSize}&keyword={request.Keyword}" +
                                             $"&languageId={request.LanguageId}");

            return data;
        }

        //Tạo 
        public async Task<bool> CreateSupplier(SupplierCreateRequest request)
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

            //if (request.ThumbnailImage != null)
            //{
            //    //chuyền file hình sang dạng Binary
            //    byte[] data;
            //    using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
            //    {
            //        data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
            //    }
            //    ByteArrayContent bytes = new ByteArrayContent(data);
            //    requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            //}

            requestContent.Add(new StringContent(request.ID_Supplier.ToString()), "iD_Supplier");
            requestContent.Add(new StringContent(request.Name_Supplier.ToString()), "name_Supplier");
            requestContent.Add(new StringContent(request.Representative.ToString()), "Representative");
            requestContent.Add(new StringContent(request.Address.ToString()), "Address");
            requestContent.Add(new StringContent(request.Phone_Number.ToString()), "Phone_Number");
            requestContent.Add(new StringContent(request.CreatedBy.ToString()), "createdBy");
            //requestContent.Add(new StringContent(request.UpdatedBy.ToString()), "updatedBy");

            var response = await client.PostAsync($"/api/suppliers/", requestContent);
            return response.IsSuccessStatusCode;
        }

        //
        public async Task<ApiResult<List<SupplierVm>>> GetAll()
        {
            return await GetAsync<ApiResult<List<SupplierVm>>>("/api/suppliers");
        }


        //cập nhật
        public async Task<bool> UpdateSupplier(SupplierUpdateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext
                                                .Session.GetString(SystemConstants.AppSettings.Token);

            //var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();


            requestContent.Add(new StringContent(request.ID_Supplier.ToString()), "iD_Supplier");
            requestContent.Add(new StringContent(request.Name_Supplier.ToString()), "name_Supplier");
            requestContent.Add(new StringContent(request.Address.ToString()), "address");
            requestContent.Add(new StringContent(request.Representative.ToString()), "representative");
            requestContent.Add(new StringContent(request.Phone_Number.ToString()), "phone_Number");
            requestContent.Add(new StringContent(request.UpdatedBy.ToString()), "updatedBy");

            var response = await client.PutAsync($"/api/suppliers/" + request.ID_Supplier, requestContent);
            return response.IsSuccessStatusCode;

        }

        ///
        public async Task<ApiResult<SupplierVm>> GetSupplierById(string supplierId)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            //truyền token đăng nhập vào ủy quyền(Authorization)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/suppliers/{supplierId}");

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<SupplierVm>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<SupplierVm>>(body);

        }

        //xóa unit
        public async Task<ApiResult<bool>> DeleteSupplier(string supplierId)
        {
            //lấy session
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            //truyền token session đăng nhập vào ủy quyền(Authorization)
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.DeleteAsync($"/api/suppliers/{supplierId}");

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body);
        }

    }
}
