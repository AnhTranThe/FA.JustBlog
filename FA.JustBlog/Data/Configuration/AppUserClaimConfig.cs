using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class AppUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<string>>

    {

        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            _ = builder.ToTable("AppUserClaims");



        }

    }
}
