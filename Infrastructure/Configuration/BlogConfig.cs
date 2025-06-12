using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(x => x.Id);

           
            builder.HasOne(x => x.UserAccount)
                   .WithMany(x => x.Blogs)
                   .HasForeignKey(x => x.UserAccountId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 