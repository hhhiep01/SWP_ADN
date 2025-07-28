using System.Threading.Tasks;
using Application.Response;
using Application.Request;

namespace Application.Interface
{
    public interface ILocusResultService
    {
        Task<ApiResponse> GetAllAsync();
        Task<ApiResponse> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(LocusResultRequest request);
        Task<ApiResponse> UpdateAsync(int id, LocusResultRequest request);
        Task<ApiResponse> DeleteAsync(int id);
        Task<ApiResponse> GetBySampleIdAsync(int sampleId);
        Task<ApiResponse> GetBySampleCodeAsync(string sampleCode);
    }
} 