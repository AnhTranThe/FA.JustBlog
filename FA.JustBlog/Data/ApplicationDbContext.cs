using FA.JustBlog.Common;
using FA.JustBlog.Data.Configuration;
using FA.JustBlog.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            _ = builder.ApplyConfiguration(new AppRoleClaimConfig());
            _ = builder.ApplyConfiguration(new AppRoleConfig());
            _ = builder.ApplyConfiguration(new AppUserClaimConfig());
            _ = builder.ApplyConfiguration(new AppUserConfig());
            _ = builder.ApplyConfiguration(new AppUserLoginConfig());
            _ = builder.ApplyConfiguration(new AppUserRoleConfig());
            _ = builder.ApplyConfiguration(new AppUserTokenConfig());
            _ = builder.ApplyConfiguration(new CategoryConfig());
            _ = builder.ApplyConfiguration(new MapPostTagConfig());
            _ = builder.ApplyConfiguration(new PostConfig());
            _ = builder.ApplyConfiguration(new TagConfig());
            _ = builder.ApplyConfiguration(new CommentConfig());

        }
        public virtual async Task<int> SaveChangesAsync(string userId = "")
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdateAt = DateTime.Now;
                    entry.Entity.Updateby = userId;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateAt = DateTime.Now;
                    entry.Entity.Createby = userId;
                }
            }


            return await base.SaveChangesAsync();

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<MapPostTag> MapPostTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }


    }
}
