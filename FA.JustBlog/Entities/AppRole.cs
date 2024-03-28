using Microsoft.AspNetCore.Identity;

namespace FA.JustBlog.Entities
{
    public class AppRole : IdentityRole<string>
    {
        public string DisplayName { get; set; } = string.Empty;
    }
}
