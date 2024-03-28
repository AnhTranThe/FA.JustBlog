using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class AppUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>

    {

        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            _ = builder.ToTable("AppUserTokens");



        }

    }
}
