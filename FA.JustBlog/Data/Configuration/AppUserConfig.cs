using FA.JustBlog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class AppUserConfig : IEntityTypeConfiguration<AppUser>

    {

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            _ = builder.ToTable("AppUsers");




        }

    }
}
