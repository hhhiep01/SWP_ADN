using Application.Request.TestOrder;
using Domain.Entity;

namespace Application.Repository
{
    public interface ITestOrderRepository : IGenericRepository<TestOrder>
    {
        Task<List<TestOrder>> SearchTestOrdersAsync(SearchTestOrderRequest req);
        Task<int> CountTestOrdersAsync(SearchTestOrderRequest req);
    }
} 