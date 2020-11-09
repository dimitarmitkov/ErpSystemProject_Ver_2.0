using System;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserViewModel userViewModel)
        {
            var userId = usersService.GetUserId(userViewModel.InputEmailAddress, userViewModel.InputEmailAddress);

            //if (userId == null)
            //{
            //    return this.Content("Incorrect email or passwort");
            //}

            //this.SignIn();
            return this.Redirect("/");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel registerUser)
        {

            if (!ModelState.IsValid)
            {
                return this.View();
            }

            this.usersService.CreateUser(registerUser);

            return this.Redirect("/Users/Login");
        }
    }
}
