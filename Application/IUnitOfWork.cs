using Application.Repository;


namespace Application
{
    public interface IUnitOfWork
    {
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


        public Task SaveChangeAsync();
        Task<T> ExecuteScalarAsync<T>(string sql);
        Task ExecuteRawSqlAsync(string sql);
    }
}
