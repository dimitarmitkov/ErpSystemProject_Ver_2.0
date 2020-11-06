using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ErpSystem.Data;
using ErpSystem.Models;

namespace ErpSystem.Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly ErpSystemDbContext dbContext;

        public UsersService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateUser(string firstName, string lastName, string email, string password)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = ComputeHash(password)
            };

            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();
        }

        public void DeleteUser(string userId, string email)
        {
            var user = this.dbContext.Users.FirstOrDefault(u => u.Id == userId && u.Email == email);

            this.dbContext.Remove(user);
            this.dbContext.SaveChanges();
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.dbContext.Users.Any(u => u.Email == email);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
