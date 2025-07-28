using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class SampleConfig : IEntityTypeConfiguration<Sample>
    {
        public void Configure(EntityTypeBuilder<Sample> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SampleStatus).IsRequired();
            builder.Property(x => x.Notes).IsRequired(false);
            builder.Property(x => x.CollectedBy).IsRequired(false);

            // Relationships
            builder.HasOne(x => x.TestOrder)
                .WithMany(x => x.Samples)
                .HasForeignKey(x => x.TestOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SampleMethod)
                .WithMany(x => x.Samples)
                .HasForeignKey(x => x.SampleMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Collector)
                .WithMany(x => x.Samples)
                .HasForeignKey(x => x.CollectedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.LocusResults)
               .WithOne(lr => lr.Sample)
               .HasForeignKey(lr => lr.SampleId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 