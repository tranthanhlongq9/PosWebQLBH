using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosWebQLBH.Application.Catalog.Products;
using PosWebQLBH.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.BackendApi.Controllers
{
    //api/products
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //http://localhost:port/product
        [HttpGet] //lấy tất cả sp
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("GetFood")] //lấy tất cả sp của id category là DA
        public async Task<IActionResult> GetFood()
        {
            var proFood = await _productService.GetFood();
            return Ok(proFood);
        }

        ////http://localhost:port/product?pageIndex=1&pageSize=10&CategoryId=
        //[HttpGet("product=")] //lấy sp theo category id
        //public async Task<IActionResult> GetByCategoryId([FromQuery] GetPublicProductPagingRequest request)
        //{
        //    var products = await _productService.GetAllByCategoryId(request);
        //    return Ok(products);
        //}

        [HttpGet("paging")] //lấy sp
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _productService.GetAllpaging(request);
            return Ok(products);
        }

        //http://localhost:port/product/pep221
        [HttpGet("{productId}")] //lấy sp theo id
        public async Task<IActionResult> GetById(string productId)
        {
            var proById = await _productService.GetById(productId);
            //if (proById == null)
            //{
            //    return BadRequest("Không tìm thấy sản phẩm");
            //}
            return Ok(proById);
        }

        [HttpPost] //thường là HttpPost vì nó tạo mới
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId == null)
                return BadRequest();

            var product = await _productService.GetById(productId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, request);
        }

        [HttpPut("{productId}")] // là HttpPut vì nó update
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] string productId, [FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            request.ID_Product = productId;
            var affectedResult = await _productService.Update(request);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}")] // là HttpDelete vì nó xóa
        public async Task<IActionResult> Delete(string productId)
        {
            var affectedResult = await _productService.Delete(productId);
            if (!affectedResult.IsSuccessed)
                return BadRequest();

            return Ok(affectedResult);
        }

        [HttpPatch("{productId}/{newPrice}")] //HttpPatch : Update 1 phần của bản ghi.. (và vì price nằm trong bảng product nên dùng update 1 phần sẽ nhanh hơn, chú trọng vào update giá thôi)
        public async Task<IActionResult> UpdatePrice(string productId, decimal newPrice)
        {
            var isSuccessfull = await _productService.UpdatePrice(productId, newPrice);
            if (isSuccessfull)
                return Ok();

            return BadRequest();
        }

        [HttpPut("{productId}/quantity+={addedQuantity}")] //HttpPut : update tồn kho
        public async Task<IActionResult> UpdateStockProduct(string productId, int addedQuantity) //update số lượng tồn kho
        {
            var isSuccessfull = await _productService.UpdateStock(productId, addedQuantity);
            if (isSuccessfull)
                return Ok();

            return BadRequest();
        }
    }
}