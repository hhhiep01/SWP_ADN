using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class UserAccountConfig : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.HasOne(x => x.Role)
                .WithMany(x => x.UserAccounts)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.TestOrders)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.AppointmentTestOrders)
                .WithOne(x => x.AppointmentStaff)
                .HasForeignKey(x => x.AppointmentStaffId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
} 