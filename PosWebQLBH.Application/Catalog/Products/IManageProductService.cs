
using PosWebQLBH.ViewModels.Catalog.ProductImages;
using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<string> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(string productId);

        Task<ProductViewModel> GetById(string productId);

        Task<bool> UpdatePrice(string productId, decimal newPrice);

        Task<bool> UpdateStock(string productId, int addedQuantity);

        Task<PagedResult<ProductViewModel>> GetAllpaging(GetManageProductPagingRequest request);

        //Task<int> AddImage(int productId, ProductImageCreateRequest request);

        //Task<int> RemoveImage(int imageId);

        //Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        //Task<ProductImageViewModel> GetImageById(int imageId);

        Task<List<ProductImageViewModel>> GetListImages(string productId);
    }
}
