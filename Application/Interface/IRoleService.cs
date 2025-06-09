using Application.Response;
using Application.Request.Role;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRoleService
    {
        Task<ApiResponse> GetAllAsync();
        Task<ApiResponse> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(RoleRequest request);
        Task<ApiResponse> UpdateAsync(int id, RoleRequest request);
        Task<ApiResponse> DeleteAsync(int id);
    }
} 