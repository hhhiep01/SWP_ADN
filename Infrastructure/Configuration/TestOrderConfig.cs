using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class TestOrderConfig : IEntityTypeConfiguration<TestOrder>
    {
        public void Configure(EntityTypeBuilder<TestOrder> builder)
        {
           
            builder.HasKey(x => x.Id);

            // Relationships
            builder.HasOne(x => x.User)
                .WithMany(x => x.TestOrders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Service)
                .WithMany(x => x.TestOrders)
                .HasForeignKey(x => x.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SampleMethod)
                .WithMany(x => x.TestOrders)
                .HasForeignKey(x => x.SampleMethodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AppointmentStaff)
                .WithMany(x => x.AppointmentTestOrders)
                .HasForeignKey(x => x.AppointmentStaffId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 