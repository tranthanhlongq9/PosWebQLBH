using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductViewModel>> GetProductPagings(GetManageProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<bool> UpdateProduct(ProductUpdateRequest request);

        Task<ApiResult<bool>> DeleteProduct(string productId);

        Task<ApiResult<ProductViewModel>> GetProductById(string productId);

        Task<ApiResult<List<ProductViewModel>>> GetAll();

        public Task<string> GetAllPro();

        Task<bool> UpdateStock(ProductUpdateRequest request);

        Task<bool> SellStock(ProductUpdateRequest request);
    }
}