using PosWebQLBH.ViewModels.Catalog.ProductImages;
using PosWebQLBH.ViewModels.Catalog.Products;
using PosWebQLBH.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Products
{
    //này làm việc với Database
    public interface IProductService
    {
        //tạo
        Task<string> Create(ProductCreateRequest request);

        //cập nhật
        Task<int> Update(ProductUpdateRequest request);

        //xóa
        Task<int> Delete(string productId);

        //lấy sp theo id
        Task<ProductViewModel> GetById(string productId);

        //cập nhật giá
        Task<bool> UpdatePrice(string productId, decimal newPrice);

        //cập nhật tồn kho
        Task<bool> UpdateStock(string productId, int addedQuantity);

        //lấy sp lên phân trang
        Task<PagedResult<ProductViewModel>> GetAllpaging(GetManageProductPagingRequest request);

        //Task<int> AddImage(int productId, ProductImageCreateRequest request);

        //Task<int> RemoveImage(int imageId);

        //Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        //Task<ProductImageViewModel> GetImageById(int imageId);

        Task<List<ProductImageViewModel>> GetListImages(string productId); //phát triển sau

        //lấy sp theo category id
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);

        //lấy tất cả
        Task<List<ProductViewModel>> GetAll();
    }
}