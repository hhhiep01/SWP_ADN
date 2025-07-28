using Domain.Entity;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=172.17.0.2; Port=5431; Database=koidelivery; Username=postgres; Password=matkhau;Include Error Detail=True;TrustServerCertificate=True");
        }
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SampleMethod> SampleMethods { get; set; }
        public DbSet<ServiceSampleMethod> ServiceSampleMethods { get; set; }
        public DbSet<TestOrder> TestOrders { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LocusResult> LocusResults { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserAccountConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new ServiceConfig());
            modelBuilder.ApplyConfiguration(new SampleMethodConfig());
            modelBuilder.ApplyConfiguration(new SampleMethodServiceConfig());
            modelBuilder.ApplyConfiguration(new TestOrderConfig());
            modelBuilder.ApplyConfiguration(new BlogConfig());
            modelBuilder.ApplyConfiguration(new SampleConfig());
            modelBuilder.ApplyConfiguration(new ResultConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new LocusResultConfig());
        }
    }   
}
