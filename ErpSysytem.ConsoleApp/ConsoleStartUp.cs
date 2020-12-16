namespace ErpSysytem.ConsoleApp
{
    using ErpSystem.Data;

    using Microsoft.EntityFrameworkCore;

    public class ConsoleStartUp
    {
        public static void Main(string[] args)
        {
            var db = new ErpSystemDbContext();

            db.Database.Migrate();
        }
    }
}
