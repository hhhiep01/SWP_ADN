using Application.Interface;
using Application.Request.SampleMethod;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleMethodController : ControllerBase
    {
        private readonly ISampleMethodService _sampleMethodService;
        public SampleMethodController(ISampleMethodService sampleMethodService)
        {
            _sampleMethodService = sampleMethodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sampleMethodService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sampleMethodService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SampleMethodRequest request)
        {
            var result = await _sampleMethodService.CreateAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SampleMethodUpdateRequest request)
        {
            var result = await _sampleMethodService.UpdateAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = "Staff,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sampleMethodService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
} 