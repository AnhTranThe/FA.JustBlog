using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class AppRoleClaimConfig : IEntityTypeConfiguration<IdentityRoleClaim<string>>

    {

        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            _ = builder.ToTable("AppRoleClaims");



        }

    }
}
