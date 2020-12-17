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
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var user = this.usersService.LoginUser(userViewModel);
            var userEmail = User.Identity.Name;

            if (usersService.IsLogged(userViewModel.Email))
            {
                return this.Redirect("/Users/AlreadyLogged");
            }

            await this.signInManager.PasswordSignInAsync(user.Email, user.PasswordHash, true, true);

            var userId = userManager.GetUserId(User);

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
            if (!ModelState.IsValid)
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
            var userId = userManager.GetUserId(User);

            var userEmail = User.Identity.Name;

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
