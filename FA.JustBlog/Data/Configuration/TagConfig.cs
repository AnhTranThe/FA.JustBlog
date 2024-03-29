using FA.JustBlog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class TagConfig : IEntityTypeConfiguration<Tag>

    {

        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            _ = builder.ToTable("Tags");


        }

    }
}
