using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosWebQLBH.Application.Partner.Suppliers;
using PosWebQLBH.ViewModels.Partner.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.BackendApi.Controllers
{
    //api/supplier
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("paging")] //lấy tất cả 
        public async Task<IActionResult> GetAllSupplierPaging([FromQuery] GetSupplierPagingRequest request)
        {
            var supplier = await _supplierService.GetAllpaging(request);
            return Ok(supplier);
        }

        //http://localhost:port/supplier/
        [HttpGet("{supplierId}")] //lấy theo id
        public async Task<IActionResult> GetById(string supplierId)
        {
            var supById = await _supplierService.GetById(supplierId);
            if (supById == null)
            {
                return BadRequest("Không tìm thấy nhà cung cấp");
            }
            return Ok(supById);
        }

        [HttpPost] //thường là HttpPost vì nó tạo mới
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] SupplierCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var supplierId = await _supplierService.Create(request);
            if (supplierId == null)
                return BadRequest();

            var product = await _supplierService.GetById(supplierId);

            return CreatedAtAction(nameof(GetById), new { id = supplierId }, request);
        }

        [HttpGet] //lấy tất cả 
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _supplierService.GetAll();
            return Ok(suppliers);
        }

        [HttpPut("{supplierId}")]
        [Consumes("multipart/form-data")]
        
        public async Task<IActionResult> Update([FromRoute] string SupplierId, [FromForm] SupplierUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.ID_Supplier = SupplierId;
            var affectedResult = await _supplierService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{SupplierId}")] // là HttpDelete vì nó xóa
        public async Task<IActionResult> Delete(string SupplierId)
        {
            var affectedResult = await _supplierService.Delete(SupplierId);
            if (!affectedResult.IsSuccessed)
                return BadRequest();

            return Ok(affectedResult);
        }
    }
}
