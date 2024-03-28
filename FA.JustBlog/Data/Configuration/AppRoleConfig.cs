using FA.JustBlog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class AppRoleConfig : IEntityTypeConfiguration<AppRole>

    {

        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            _ = builder.ToTable("AppRoles");




        }

    }
}
