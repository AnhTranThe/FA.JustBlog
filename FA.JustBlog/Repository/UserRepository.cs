using FA.JustBlog.Common;
using FA.JustBlog.Data;
using FA.JustBlog.Entities;
using FA.JustBlog.Interface;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Security.Claims;

namespace FA.JustBlog.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public IQueryable<AppUser> User => _context.Users;

        public UserRepository(ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager) : base()
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

        }


        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }

        public async Task RegisterUserAsync(AppUser user, string password)
        {



            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {

                    // Check if the role exists, if not create it
                    bool roleExists = await IsExistedRoleName(ConstantSystem.RoleUserName);
                    if (!roleExists)
                    {
                        _ = await CreateRoleAsync(ConstantSystem.RoleUserName);
                    }
                    // Add user to the role
                    await AssignRoleAsync(user, ConstantSystem.RoleUserName);
                }

                _ = await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }


        public async Task AssignRoleAsync(AppUser user, string RoleName)
        {
            if (user != null)
            {
                _ = await _userManager.AddToRoleAsync(user, RoleName);

            }

        }
        public async Task<bool> CreateRoleAsync(string newRoleName)
        {
            AppRole newRole = new()
            {
                Name = newRoleName,
                DisplayName = newRoleName
            };
            IdentityResult result = await _roleManager.CreateAsync(newRole);
            return result.Succeeded;
        }


        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public async Task<AppUser?> FindByEmailAsync(string Email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(Email);
            return user;


        }

        public async Task<bool> PasswordSignInAsync(string Username, string password, bool rememberMe)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(Username, password, rememberMe, false);
            return result.Succeeded;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> IsExistedRoleName(string roleName)
        {
            bool result = await _roleManager.RoleExistsAsync(roleName);
            return result;
        }

        public async Task<AppRole?> GetRoleByUserId(string UserId)
        {


            AppUser? user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {

                return null;
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);


            if (roles.Any())
            {
                string roleName = roles.First();

                AppRole? role = await _roleManager.FindByNameAsync(roleName);

                return role;
            }

            return null;
        }
    }
}
