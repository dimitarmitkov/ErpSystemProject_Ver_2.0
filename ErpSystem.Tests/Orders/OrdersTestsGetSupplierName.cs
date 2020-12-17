namespace ErpSystem.Tests.Orders
{
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services.Services;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class OrdersTestsGetSupplierName
    {
        [Fact]
        public void CheckCorrectGetSupplierName()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testGetSupplierName");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var order = new OrdersService(dbContext);

            var supplierForOrder1 = new SupplierForOrder
            {
                Id = 1,
                SupplierName = "SomeSupplier1",
            };

            var supplierForOrder2 = new SupplierForOrder
            {
                Id = 2,
                SupplierName = "SomeSupplier2",
            };

            dbContext.SupplierForOrders.Add(supplierForOrder1);
            dbContext.SupplierForOrders.Add(supplierForOrder2);
            dbContext.SaveChanges();

            var result = order.GetSupplierName();

            Assert.Equal("SomeSupplier1", result);
        }
    }
}
