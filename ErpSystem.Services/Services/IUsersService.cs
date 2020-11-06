using System;
namespace ErpSystem.Services.Services
{
    public interface IUsersService
    {

        void CreateUser(string firstName, string lastName, string email, string password);

        void DeleteUser(string userId, string email);

        bool IsEmailAvailable(string email);
    }
}
