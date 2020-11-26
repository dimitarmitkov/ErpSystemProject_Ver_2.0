using System;
using System.Threading.Tasks;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IUsersService usersService;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUsersService usersService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.usersService = usersService;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel userViewModel)
        {

            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var user = usersService.LoginUser(userViewModel);

            await this.signInManager.PasswordSignInAsync(user.Email, user.PasswordHash, true, true);

            return this.Redirect("/Home/Index");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUser)
        {

            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var user = usersService.CreateUser(registerUser);

            var result = await this.userManager.CreateAsync(user, user.PasswordHash);

            return this.Redirect("/Users/Login");
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect("/Users/Login");
        }
    }
}
