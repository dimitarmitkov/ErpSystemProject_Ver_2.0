using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace ErpSystem.Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsersService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IdentityUser CreateUser(RegisterUserViewModel registerUser)
        {
            var user = new IdentityUser
            {
                Email = registerUser.Email,
                UserName = registerUser.Email,
                PasswordHash = registerUser.Password,
            };

            return user;
        }

        public IdentityUser LoginUser(LoginUserViewModel userViewModel)
        {
            var user = new IdentityUser
            {
                Email = userViewModel.Email,
                PasswordHash = userViewModel.Password,
            };

            return user;


        }
    }
}
