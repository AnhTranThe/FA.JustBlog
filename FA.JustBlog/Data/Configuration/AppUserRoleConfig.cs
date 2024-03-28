using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class AppUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>

    {

        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            _ = builder.ToTable("AppUserRoles");



        }

    }
}
