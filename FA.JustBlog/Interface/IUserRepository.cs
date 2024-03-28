using FA.JustBlog.Entities;
using System.Security.Claims;

namespace FA.JustBlog.Interface
{
    public interface IUserRepository
    {
        IQueryable<AppUser> User { get; }
        Task RegisterUserAsync(AppUser user, string password);
        Task AssignRoleAsync(AppUser user, string roleName);
        Task<bool> IsExistedRoleName(string roleName);
        Task<bool> CreateRoleAsync(string newRoleName);
        Task<AppRole?> GetRoleByUserId(string UserId);
        bool IsSignedIn(ClaimsPrincipal claim);
        bool IsValidEmail(string emailaddress);
        Task<AppUser?> FindByEmailAsync(string Email);
        Task<bool> PasswordSignInAsync(string Username, string password, bool rememberMe);
        Task SignOutAsync();




    }
}
