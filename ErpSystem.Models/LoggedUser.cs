namespace ErpSystem.Models
{
    public class LoggedUser
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public bool IsLogged { get; set; }
    }
}