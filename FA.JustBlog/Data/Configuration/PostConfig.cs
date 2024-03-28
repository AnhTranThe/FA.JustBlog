using FA.JustBlog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class PostConfig : IEntityTypeConfiguration<Post>

    {

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            _ = builder.ToTable("Posts");
            _ = builder.HasOne(x => x.User).WithMany(x => x.Posts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            _ = builder.HasOne(x => x.Category).WithMany(x => x.Posts).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);

        }

    }
}
