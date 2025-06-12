using Application.Request.Blog;
using Application.Response;

namespace Application.Interface
{
    public interface IBlogService
    {
        Task<ApiResponse> GetAllAsync();
        Task<ApiResponse> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(CreateBlogRequest request);
        Task<ApiResponse> UpdateAsync(UpdateBlogRequest request);
        Task<ApiResponse> DeleteAsync(int id);
    }
} 