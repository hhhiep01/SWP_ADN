using Application.Response;
using Application.Request.Service;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IServiceService
    {
        Task<ApiResponse> GetAllAsync();
        Task<ApiResponse> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(ServiceRequest request);
        Task<ApiResponse> UpdateAsync(ServiceUpdateRequest request);
        Task<ApiResponse> DeleteAsync(int id);
    }
} 