using Application.Interface;
using Application.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;
        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _resultService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _resultService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResultRequest request)
        {
            var result = await _resultService.CreateAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ResultRequest request)
        {
            var result = await _resultService.UpdateAsync(id, request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _resultService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("user-history")]
        public async Task<IActionResult> GetMyResults()
        {
            var result = await _resultService.GetByCurrentUserAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("testorder/{testOrderId}/full-result")]
        public async Task<IActionResult> GetFullResultByTestOrderId(int testOrderId)
        {
            var result = await _resultService.GetFullResultByTestOrderIdAsync(testOrderId);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

    }
} 