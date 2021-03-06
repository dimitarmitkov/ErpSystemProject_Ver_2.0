﻿namespace ErpSystem.Services.Services
{
    using System.Threading.Tasks;
    using ErpSystem.Services.ViewModels.User;
    using Microsoft.AspNetCore.Identity;

    public interface IUsersService
    {
        IdentityUser CreateUser(RegisterUserViewModel registerUser);

        IdentityUser LoginUser(LoginUserViewModel userViewModel);

        bool IsLogged(string userEmail);

        Task UserLogInDeleteRecord(string userEmail);

        Task UserLogInRecord(string userEmail, string userId);
    }
}
