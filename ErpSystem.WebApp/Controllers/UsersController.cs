namespace ErpSystem.WebApp.Controllers
{
    using System.Threading.Tasks;
    using ErpSystem.Services.Services;
    using ErpSystem.Services.ViewModels.User;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = this.usersService.LoginUser(userViewModel);
            var userEmail = this.User.Identity.Name;

            if (this.usersService.IsLogged(userViewModel.Email))
            {
                return this.Redirect("/Users/AlreadyLogged");
            }

            await this.signInManager.PasswordSignInAsync(user.Email, user.PasswordHash, true, true);

            var userId = this.userManager.GetUserId(this.User);

            await this.usersService.UserLogInRecord(user.Email, userId);

            return this.Redirect("/Home/Index");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUser)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = this.usersService.CreateUser(registerUser);

            var result = await this.userManager.CreateAsync(user, user.PasswordHash);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, "User");
            }

            return this.Redirect("/Users/Login");
        }

        public async Task<IActionResult> Logout()
        {
            var userId = this.userManager.GetUserId(this.User);

            var userEmail = this.User.Identity.Name;

            await this.usersService.UserLogInDeleteRecord(userEmail);

            await this.signInManager.SignOutAsync();

            return this.Redirect("/Users/Login");
        }

        public IActionResult AlreadyLogged()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult AlreadyLogged(string id)
        {
            return this.Redirect("/Users/Login");
        }
    }
}
