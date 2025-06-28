using Application.Interface;
using Application.Request.Sample;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _sampleService;
        public SampleController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sampleService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sampleService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SampleRequest request)
        {
            var result = await _sampleService.CreateAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSampleRequest request)
        {
            var result = await _sampleService.UpdateAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sampleService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("test-order/{testOrderId}")]
        public async Task<IActionResult> GetByTestOrderId(int testOrderId)
        {
            var result = await _sampleService.GetByTestOrderIdAsync(testOrderId);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("collector/{collectorId}")]
        public async Task<IActionResult> GetByCollectorId(int collectorId)
        {
            var result = await _sampleService.GetByCollectorIdAsync(collectorId);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
} 