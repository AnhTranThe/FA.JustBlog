using FA.JustBlog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class CommentConfig : IEntityTypeConfiguration<Comment>

    {

        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            _ = builder.ToTable("Comments");
            _ = builder.HasOne(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.ClientSetNull);
            _ = builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.ClientSetNull);

        }

    }
}
