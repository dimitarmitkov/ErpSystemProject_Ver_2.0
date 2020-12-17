namespace ErpSystem.Tests.Users
{
    using ErpSystem.Data;
    using ErpSystem.WebApp.Areas.Identity.Data;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UsersTestsCreateUser
    {
        [Fact]
        public void CheckCorrectGetSupplierName()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testCreateUser");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var optionsBuildeUser = new DbContextOptionsBuilder<IdentityDataContext>();
            var userDbContex = new IdentityDataContext(optionsBuildeUser.Options);
        }
    }
}
