using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                   .IsRequired();

            builder.HasOne(x => x.Blog)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.BlogId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.UserAccount)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(x => x.UserAccountId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 