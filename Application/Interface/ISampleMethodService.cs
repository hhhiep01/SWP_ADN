using Application.Response;
using Application.Request.Service;
using System.Threading.Tasks;
using Application.Request.SampleMethod;

namespace Application.Interface
{
    public interface ISampleMethodService
    {
        Task<ApiResponse> GetAllAsync();
        Task<ApiResponse> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(SampleMethodRequest request);
        Task<ApiResponse> UpdateAsync(SampleMethodUpdateRequest request);
        Task<ApiResponse> DeleteAsync(int id);
    }
} 