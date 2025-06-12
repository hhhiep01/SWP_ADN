using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class SampleMethodConfig : IEntityTypeConfiguration<SampleMethod>
    {
        public void Configure(EntityTypeBuilder<SampleMethod> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(x => x.ServiceSampleMethods)
                .WithOne(x => x.SampleMethod)
                .HasForeignKey(x => x.SampleMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.TestOrders)
                .WithOne(x => x.SampleMethod)
                .HasForeignKey(x => x.SampleMethodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 