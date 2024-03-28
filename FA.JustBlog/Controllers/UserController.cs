using FA.JustBlog.Entities;
using FA.JustBlog.Interface;
using FA.JustBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace FA.JustBlog.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser? user = await _userRepository.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    MailAddress address = new(model.Email);
                    string userName = address.User;
                    string FirstName = model.FirstName.ToUpper();
                    string LastName = model.LastName.ToUpper();


                    _ = _userRepository.RegisterUserAsync(new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = userName,
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = model.Email,
                        EmailConfirmed = true,


                    }, model.Password);
                    return RedirectToAction("Login");
                }


                else
                {
                    ModelState.AddModelError("", "There is already an email or username belonging to such a user.");
                }

            }

            return View(model);
        }

        public IActionResult Login()
        {
            return User.Identity!.IsAuthenticated ? RedirectToAction("Index", "Posts") : View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {


            if (ModelState.IsValid)
            {

                AppUser? ExisitedUser = await _userRepository.FindByEmailAsync(model.Email);


                if (ExisitedUser != null)
                {
                    if (ExisitedUser.UserName != null)
                    {
                        bool result = await _userRepository.PasswordSignInAsync(ExisitedUser.UserName, model.Password, model.RememberMe);
                        return result ? RedirectToAction("Index", "Posts") : (IActionResult)RedirectToAction("Login", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User name is null!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong password or email!");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userRepository.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }



    }
}
