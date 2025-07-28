using System.Threading.Tasks;

using Application.Request;
using Application.Response;

namespace Application.Interface
{
    public interface IResultService
    {
        Task<ApiResponse> GetAllAsync();
        Task<ApiResponse> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(ResultRequest request);
        Task<ApiResponse> UpdateAsync(int id, ResultRequest request);
        Task<ApiResponse> DeleteAsync(int id);
        Task<ApiResponse> GetByCurrentUserAsync();
        Task<ApiResponse> GetFullResultByTestOrderIdAsync(int testOrderId);
    }
} 