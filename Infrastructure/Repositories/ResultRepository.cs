using Application.Repository;
using Domain.Entity;


namespace Infrastructure.Repositories
{
    public class ResultRepository : GenericRepository<Result>, IResultRepository
    {
        public ResultRepository(AppDbContext context) : base(context)
        {
        }
    }
} 