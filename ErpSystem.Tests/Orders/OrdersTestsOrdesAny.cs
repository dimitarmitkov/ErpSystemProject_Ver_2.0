namespace ErpSystem.Tests.Orders
{
    using System;
    using System.Linq;
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services.Services;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class OrdersTestsOrdesAny
    {
        [Fact]
        public void CheckCorrectTestsOrdesAny()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testTestsOrdesAny");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var order = new OrdersService(dbContext);

            var createOrder1 = new Order
            {
                SupplierId = 100,
                Supplier = "Supplier1",
                ProductId = 1001,
                ProductName = "Product 1001",
                OrderDate = DateTime.UtcNow.Date,
                NumberOfTransportPackageUnitsOrdered = 10,
                TotalAmountOfOrder = 1111m,
                TotalOrderWeight = 100,
            };
            dbContext.Orders.Add(createOrder1);

            var createOrder2 = new Order
            {
                SupplierId = 100,
                Supplier = "Supplier1",
                ProductId = 1002,
                ProductName = "Product 1002",
                OrderDate = DateTime.UtcNow.Date,
                NumberOfTransportPackageUnitsOrdered = 20,
                TotalAmountOfOrder = 23960m,
                TotalOrderWeight = 80000,
            };
            dbContext.Orders.Add(createOrder2);
            dbContext.SaveChanges();

            var result = order.OrdesAny().ToList();

            Assert.True(result.Count() == 2);
            Assert.True(result[0] == 1001);
            Assert.True(result[1] == 1002);
        }
    }
}
