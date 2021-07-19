﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosWebQLBH.Application.Catalog.Products;
using PosWebQLBH.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;

        public ProductController(IPublicProductService publicProductService, 
                                 IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        //http://localhost:port/product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }

        //http://localhost:port/product/public-paging
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(request);
            return Ok(products);
        }

        //http://localhost:port/product/pep221
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(string productId)
        {
            var product = await _manageProductService.GetById(productId);
            if (product == null)
            { 
                return BadRequest("Không tìm thấy sản phẩm");
            }
            return Ok(product);
        }

        [HttpPost] //thường là HttpPost vì nó tạo mới
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            var productId = await _manageProductService.Create(request);
            if (productId == null)
                return BadRequest();

            var product = await _manageProductService.GetById(productId);

            return CreatedAtAction(nameof(GetById), new { id = productId } , request);
        }

        [HttpPut] // là HttpPut vì nó update
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var affectedResult = await _manageProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}")] // là HttpDelete vì nó xóa
        public async Task<IActionResult> Delete(string productId)
        {
            var affectedResult = await _manageProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")] //HttpPatch : Update 1 phần của bản ghi
        public async Task<IActionResult> UpdatePrice(string productId, decimal newPrice)
        {
            var isSuccessfull = await _manageProductService.UpdatePrice(productId, newPrice);
            if (isSuccessfull)
                return Ok();

            return BadRequest();
        }

    }
}
