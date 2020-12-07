using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ErpSystem.Data;
using ErpSystem.Data.Migrations;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace ErpSystem.Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ErpSystemDbContext dbContext;

        public UsersService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ErpSystemDbContext dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
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

        public async Task UserLogInRecord(string userEmail, string userId)
        {


            var exists = this.dbContext.LoggedUsers.Any(u => u.UserName == userEmail);

            if (exists)
            {
                var currentLoggedUser = this.dbContext.LoggedUsers.Where(u => u.UserName == userEmail).FirstOrDefault();
                currentLoggedUser.IsLogged = true;
                this.dbContext.LoggedUsers.Update(currentLoggedUser);
            }
            else
            {
                var loggedUser = new LoggedUser
                {
                    UserName = userEmail,
                    UserId = userId,
                    IsLogged = true
                };

                await this.dbContext.LoggedUsers.AddAsync(loggedUser);
            }
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UserLogInDeleteRecord(string userEmail)
        {
            var user = this.dbContext.LoggedUsers.Where(u => u.UserName == userEmail).FirstOrDefault();

            user.IsLogged = false;

            this.dbContext.LoggedUsers.Update(user);
            await this.dbContext.SaveChangesAsync();
        }

        public bool IsLogged(string userEmail)
        {
            if (!string.IsNullOrEmpty(userEmail))
            {
                return this.dbContext.LoggedUsers.Where(u => u.UserName == userEmail).Select(x => x.IsLogged).FirstOrDefault();
            }

            return false;
        }
    }
}
