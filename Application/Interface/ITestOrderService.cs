using Application.Request.TestOrder;
using Application.Response;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ITestOrderService
    {
        //Task<ApiResponse> GetAllAsync();
        Task<ApiResponse> GetAllAsync(SearchTestOrderRequest req);
        Task<ApiResponse> GetByIdAsync(int id);
        Task<ApiResponse> CreateAsync(CreateTestOrderRequest request);
        Task<ApiResponse> UpdateAsync(UpdateTestOrderRequest request);
        Task<ApiResponse> DeleteAsync(int id);
        Task<ApiResponse> UpdateStatusAsync(UpdateTestOrderStatusRequest request);
        Task<ApiResponse> UpdateDeliveryKitStatusAsync(UpdateDeliveryKitStatusRequest request);
    }
} 