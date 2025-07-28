using Application.Interface;
using Application.Request;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocusResultController : ControllerBase
    {
        private readonly ILocusResultService _locusResultService;
        public LocusResultController(ILocusResultService locusResultService)
        {
            _locusResultService = locusResultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _locusResultService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _locusResultService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("by-sample/{sampleId}")]
        public async Task<IActionResult> GetBySampleId(int sampleId)
        {
            var result = await _locusResultService.GetBySampleIdAsync(sampleId);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocusResultRequest request)
        {
            var result = await _locusResultService.CreateAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{sampleId}")]
        public async Task<IActionResult> Update(int sampleId, [FromBody] LocusResultRequest request)
        {
            var result = await _locusResultService.UpdateAsync(sampleId, request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _locusResultService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpGet("by-sample-code/{sampleCode}")]
        public async Task<IActionResult> GetBySampleCode(string sampleCode)
        {
            var result = await _locusResultService.GetBySampleCodeAsync(sampleCode);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
