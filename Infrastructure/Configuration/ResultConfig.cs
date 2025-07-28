using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ResultConfig : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.TestOrder)
                .WithOne(t => t.Result)
                .HasForeignKey<Result>(r => r.TestOrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 