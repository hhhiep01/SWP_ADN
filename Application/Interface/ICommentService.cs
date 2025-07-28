using Application.Request;
using Application.Response;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ICommentService
    {
        Task<ApiResponse> GetAllAsync();
        Task<ApiResponse> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(CommentRequest request);
        Task<ApiResponse> UpdateAsync(UpdateCommentRequest request);
        Task<ApiResponse> DeleteAsync(int id);
        Task<ApiResponse> GetByBlogIdAsync(int blogId);
    }
} 