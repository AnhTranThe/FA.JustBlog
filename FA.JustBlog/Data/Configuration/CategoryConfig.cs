using FA.JustBlog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class CategoryConfig : IEntityTypeConfiguration<Category>

    {

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            _ = builder.ToTable("Categories");


        }

    }
}
