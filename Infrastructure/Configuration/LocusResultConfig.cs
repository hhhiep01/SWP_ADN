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
    public class LocusResultConfig : IEntityTypeConfiguration<LocusResult>
    {
        public void Configure(EntityTypeBuilder<LocusResult> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(x => x.Sample)
              .WithMany(lr => lr.LocusResults)
              .HasForeignKey(lr => lr.SampleId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
