using Application.Repository;
using Domain.Entity;

namespace Infrastructure.Repositories
{
    public class SampleRepository : GenericRepository<Sample>, ISampleRepository
    {
        public SampleRepository(AppDbContext context) : base(context)
        {
        }
    }
} 