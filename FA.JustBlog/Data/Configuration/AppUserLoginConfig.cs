using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Data.Configuration
{

    public class AppUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>

    {

        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            _ = builder.ToTable("AppUserLogins");



        }

    }
}
