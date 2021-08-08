using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosWebQLBH.Application.Partner.Customers;
using PosWebQLBH.ViewModels.Partner.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.BackendApi.Controllers
{
    //api/customers
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("paging")] //lấy tất cả kh
        public async Task<IActionResult> GetAllCustomerPaging([FromQuery]GetManageCustomerPagingRequest request)
        {
            var customer = await _customerService.GetAllpaging(request);
            return Ok(customer);
        }

        /*[HttpGet("{customerId}")] //lấy kh theo id
        public async Task<IActionResult> GetById(long customerId)
        {
            var proById = await _customerService.GetById(customerId);
            if (proById == null)
            {
                return BadRequest("Không tìm thấy khách hàng");
            }
            return Ok(proById);
        }*/

        /*[HttpPost] //thường là HttpPost vì nó tạo mới
        public async Task<IActionResult> Create([FromForm] CustomerCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerId = await _customerService.Create(request);
            if (customerId == null)
                return BadRequest();

            var customer = await _customerService.GetById(customerId);

            return CreatedAtAction(nameof(GetById), new { id = customerId }, request);
        }*/
    }
}
