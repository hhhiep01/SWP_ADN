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

            builder.HasOne(r => r.Sample)
                .WithOne(s => s.Result)
                .HasForeignKey<Result>(r => r.SampleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 