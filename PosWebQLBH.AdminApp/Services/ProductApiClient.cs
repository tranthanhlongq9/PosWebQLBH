using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.Utilities.Constants;
using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.ViewModels.Common;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                                    IHttpContextAccessor httpContextAccessor)
                    : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        //Tạo SP
        public async Task<bool> CreateProduct(ProductCreateRequest request)
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

            if (request.ThumbnailImage != null)
            {
                //chuyền file hình sang dạng Binary
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.ID_Product.ToString()), "iD_Product");
            requestContent.Add(new StringContent(request.ID_Category.ToString()), "iD_Category");
            requestContent.Add(new StringContent(request.Name_Product.ToString()), "name_Product");
            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.ID_Unit.ToString()), "iD_Unit");
            requestContent.Add(new StringContent(request.Length.ToString()), "length");
            requestContent.Add(new StringContent(request.Width.ToString()), "width");
            requestContent.Add(new StringContent(request.Height.ToString()), "height");
            requestContent.Add(new StringContent(request.Weight.ToString()), "weight");
            requestContent.Add(new StringContent(request.CreatedBy.ToString()), "createdBy");
            //requestContent.Add(new StringContent(request.UpdatedBy.ToString()), "updatedBy");
            requestContent.Add(new StringContent(request.Quantity.ToString()), "quantity");

            var response = await client.PostAsync($"/api/products/", requestContent);
            return response.IsSuccessStatusCode;
        }

        //show sp và phân trang
        public async Task<PagedResult<ProductViewModel>> GetProductPagings(GetManageProductPagingRequest request)
        {
            var data = await GetAsync<PagedResult<ProductViewModel>>(
                                            $"/api/products/paging?pageIndex={request.PageIndex}" +
                                            $"&pageSize={request.PageSize}&keyword={request.Keyword}" +
                                            $"&languageId={request.LanguageId}&categoryId={request.CategoryId}");

            return data;
        }
    }
}