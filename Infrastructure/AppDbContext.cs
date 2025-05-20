using Domain.Entity;
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
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=172.17.0.2; Port=5431; Database=koidelivery; Username=postgres; Password=matkhau;Include Error Detail=True;TrustServerCertificate=True");
        }
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }   
}
