

using Application.Repository;
using Domain.Entity;

namespace Infrastructure.Repositories
{
    public class ServiceSampleMethodRepository : GenericRepository<ServiceSampleMethod>, IServiceSampleMethodRepository
    {
        public ServiceSampleMethodRepository(AppDbContext context) : base(context)
        {
        }
    }
}