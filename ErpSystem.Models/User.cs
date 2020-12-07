using System;
namespace ErpSystem.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public bool IsLogged { get; set; }
    }
}
