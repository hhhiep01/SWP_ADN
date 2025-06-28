using Application.Request.Sample;
using Application.Response;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ISampleService
    {
        Task<ApiResponse> GetAllAsync();
        Task<ApiResponse> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(SampleRequest request);
        Task<ApiResponse> UpdateAsync(UpdateSampleRequest request);
        Task<ApiResponse> DeleteAsync(int id);
        Task<ApiResponse> GetByTestOrderIdAsync(int testOrderId);
        Task<ApiResponse> GetByCollectorIdAsync(int collectorId);
    }
} 