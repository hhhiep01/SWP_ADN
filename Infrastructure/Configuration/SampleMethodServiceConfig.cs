using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class SampleMethodServiceConfig : IEntityTypeConfiguration<ServiceSampleMethod>
    {
        public void Configure(EntityTypeBuilder<ServiceSampleMethod> builder)
        {
            builder.HasKey(s => s.Id);

            // Configure relationships
            builder.HasOne(s => s.Service)
                .WithMany(s => s.ServiceSampleMethods)
                .HasForeignKey(s => s.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.SampleMethod)
                .WithMany(s => s.ServiceSampleMethods)
                .HasForeignKey(s => s.SampleMethodId)
                .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
} 