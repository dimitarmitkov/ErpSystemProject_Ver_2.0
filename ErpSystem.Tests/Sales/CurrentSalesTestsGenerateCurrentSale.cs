//using System.Linq;
//using ErpSystem.Data;
//using ErpSystem.Services.Services;
//using ErpSystem.Services.ViewModels.CurrentSale;
//using Microsoft.EntityFrameworkCore;
//using Xunit;

//namespace ErpSystem.Tests.Sales
//{
//    public class CurrentSalesTestsGenerateCurrentSale
//    {
//        [Fact]
//        public void CheckCorrectGenerateCurrentSale()
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testGenerateCurrentSale");
//            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
//            var currentSale = new CurrentSalesService(dbContext);

//            var currentSaleCustomer = new CurrentSaleViewModel
//            {
//                CustomerId = "1",
//                CustomerName = "Customer1",
//                CustomerEikNumber = "BG123456789",
//                HasCustomerDiscount = true,
//            };

//            currentSale.GenerateCurrentSale(currentSaleCustomer);

//            var result = dbContext.CurrentSales.Select(x => x).ToList();

//            //Assert.True(result.Count() == 1);
//            //Assert.True(result[0].CustomerEikNumber == "BG123456789");
//        }
//    }
//}