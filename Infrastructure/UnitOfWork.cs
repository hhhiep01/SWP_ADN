using Application;
using Application.Repository;
using Domain.Entity;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private AppDbContext _context;
        public IUserAccountRepository UserAccounts { get; }
        public IEmailVerificationRepository EmailVerifications { get; }
        public IRoleRepository Roles { get; }
        public IServiceRepository Services { get; }
        public ISampleMethodRepository SampleMethods { get; }
        public IServiceSampleMethodRepository ServiceSampleMethods { get; }
        public ITestOrderRepository TestOrders { get; }
        public IBlogRepository Blogs { get; }
        public ISampleRepository Samples { get; }
        public IResultRepository Results { get; }
        public ICommentRepository Comments { get; }

        public ILocusResultRepository Locus { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            UserAccounts = new UserAccountRepository(context);
            EmailVerifications = new EmailVerificationRepository(context);
            Roles = new RoleRepository(context);
            Services = new ServiceRepository(context);
            SampleMethods = new SampleMethodRepository(context);
            ServiceSampleMethods = new ServiceSampleMethodRepository(context);
            TestOrders = new TestOrderRepository(context);
            Blogs = new BlogRepository(context);
            Samples = new SampleRepository(context);
            Results = new ResultRepository(context);
            Comments = new CommentRepository(context);
            Locus = new LocusResultRepository(context);
        }
        public async Task SaveChangeAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<T> ExecuteScalarAsync<T>(string sql)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteRawSqlAsync(string sql)
        {
            throw new NotImplementedException();
        }
    }
    
}
