
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosWebQLBH.Application.Catalog.Units;
using PosWebQLBH.ViewModels.Catalog.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.BackendApi.Controllers
{
    //api/unit
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UnitsController : Controller

    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet] //lấy tất cả category
        public async Task<IActionResult> GetAllUnit()
        {
            var units = await _unitService.GetAll();
            return Ok(units);
        }

        [HttpGet("paging")] //lấy tất cả phân trang
        public async Task<IActionResult> GetAllUnitPaging([FromQuery] GetUnitPagingRequest request)
        {
            var unit = await _unitService.GetAllpaging(request);
            return Ok(unit);
        }

        //http://localhost:port/unit/
        [HttpGet("{unitId}")] //lấy theo id
        public async Task<IActionResult> GetById(string unitId)
        {
            var uById = await _unitService.GetById(unitId);
            if (uById == null)
            {
                return BadRequest("Không tìm thấy đơn vị tính");
            }
            return Ok(uById);
        }

        [HttpPost] //thường là HttpPost vì nó tạo mới
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] UnitCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var unitId = await _unitService.Create(request);
            if (unitId == null)
                return BadRequest();

            var unit = await _unitService.GetById(unitId);

            return CreatedAtAction(nameof(GetById), new { id = unitId }, request);
        }

       
        //http: //localhost/api/unit/id
        [HttpPut("{unitId}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] string unitId, [FromForm] UnitUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.ID_Unit= unitId;
            var affectedResult = await _unitService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{unitId}")] // là HttpDelete vì nó xóa
        public async Task<IActionResult> Delete(string unitId)
        {
            var affectedResult = await _unitService.Delete(unitId);
            if (!affectedResult.IsSuccessed)
                return BadRequest();

            return Ok(affectedResult);
        }

    }
}
