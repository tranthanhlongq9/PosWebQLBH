
using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.ViewModels.Common;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<string> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(string productId);

        Task<ProductViewModel> GetById(string productId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        //Task<bool> UpdateStock(int productId, int addedQuantity);

        //Task AddViewcount(int productId);

        Task<PagedResult<ProductViewModel>> GetAllpaging(GetManageProductPagingRequest request);

        //Task<int> AddImage(int productId, ProductImageCreateRequest request);

        //Task<int> RemoveImage(int imageId);

        //Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        //Task<ProductImageViewModel> GetImageById(int imageId);

        //Task<List<ProductImageViewModel>> GetListImages(int productId);
    }
}
