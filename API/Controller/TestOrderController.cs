using Application.Interface;
using Application.Request.TestOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestOrderController : ControllerBase
    {
        private readonly ITestOrderService _testOrderService;

        public TestOrderController(ITestOrderService testOrderService)
        {
            _testOrderService = testOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SearchTestOrderRequest req)
        {
            var result = await _testOrderService.GetAllAsync(req);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _testOrderService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [Authorize(Roles = "Staff,Admin,Customer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTestOrderRequest request)
        {
            var result = await _testOrderService.CreateAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTestOrderRequest request)
        {
            var result = await _testOrderService.UpdateAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _testOrderService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateTestOrderStatusRequest request)
        {
            var result = await _testOrderService.UpdateStatusAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Staff,Admin,Customer")]
        [HttpPut("delivery-kit-status")]
        public async Task<IActionResult> UpdateDeliveryKitStatus([FromBody] UpdateDeliveryKitStatusRequest request)
        {
            var result = await _testOrderService.UpdateDeliveryKitStatusAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("customer")]
        public async Task<IActionResult> GetByCurrentCustomer()
        {
            var result = await _testOrderService.GetByCurrentCustomerAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}