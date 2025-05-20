using Application.Repository;
using Domain.Entity;


namespace Infrastructure.Repositories
{
    public class EmailVerificationRepository : GenericRepository<EmailVerification>, IEmailVerificationRepository
    {
        public EmailVerificationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
