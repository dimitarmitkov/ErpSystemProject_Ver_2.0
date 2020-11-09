using ErpSystem.Services.ViewModels.User;

namespace ErpSystem.Services.Services
{
    public interface IUsersService
    {

        void CreateUser(RegisterUserViewModel registerUser);

        void DeleteUser(string userId, string email);

        bool IsEmailAvailable(string email);

        string GetUserId(string userEmil, string userPassword);
    }
}
