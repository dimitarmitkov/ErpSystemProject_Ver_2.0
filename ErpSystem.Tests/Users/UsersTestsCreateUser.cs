using ErpSystem.Data;
using ErpSystem.Services.Services;
using ErpSystem.WebApp.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ErpSystem.Tests.Users
{
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
