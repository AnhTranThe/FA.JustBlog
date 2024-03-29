using FA.JustBlog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class MapPostTagConfig : IEntityTypeConfiguration<MapPostTag>

    {

        public void Configure(EntityTypeBuilder<MapPostTag> builder)
        {
            _ = builder.ToTable("MapPostTags");
            _ = builder.HasKey(x => new { x.PostId, x.TagId });
            _ = builder.HasOne(x => x.Post).WithMany(x => x.mapPostTags).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.ClientSetNull);
            _ = builder.HasOne(x => x.Tag).WithMany(x => x.mapPostTags).HasForeignKey(x => x.TagId).OnDelete(DeleteBehavior.ClientSetNull);




        }

    }
}
