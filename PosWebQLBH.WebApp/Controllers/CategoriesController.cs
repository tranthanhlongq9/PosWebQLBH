using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosWebQLBH.Application.Catalog.Categories;
using PosWebQLBH.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.BackendApi.Controllers
{
    //api/cate
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet("paging")] //lấy tất cả phân trang
        public async Task<IActionResult> GetAllPaging([FromQuery] GetCategoryPagingRequest request)
        {
            var cate = await _categoryService.GetAllpaging(request);
            return Ok(cate);
        }

        [HttpGet] //lấy tất cả category
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        //http://localhost:port/unit/
        [HttpGet("{categoryId}")] //lấy theo id
        public async Task<IActionResult> GetById(string categoryId)
        {
            var cateById = await _categoryService.GetById(categoryId);
            //if (cateById == null)
            //{
            //    return BadRequest("Không tìm thấy ngành hàng");
            //}
            return Ok(cateById);
        }

        [HttpPost] //thường là HttpPost vì nó tạo mới
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cateId = await _categoryService.Create(request);
            if (cateId == null)
                return BadRequest();

            var cate = await _categoryService.GetById(cateId);

            return CreatedAtAction(nameof(GetById), new { id = cateId }, request);
        }

        //http: //localhost/api/unit/id
        [HttpPut("{categoryId}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] string categoryId, [FromForm] CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.ID_Category = categoryId;
            var affectedResult = await _categoryService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{categoryId}")] // là HttpDelete vì nó xóa
        public async Task<IActionResult> Delete(string categoryId)
        {
            var affectedResult = await _categoryService.Delete(categoryId);
            if (!affectedResult.IsSuccessed)
                return BadRequest();

            return Ok(affectedResult);
        }

    }
}