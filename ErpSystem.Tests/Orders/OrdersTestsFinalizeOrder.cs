namespace ErpSystem.Tests.Orders
{
    using System.Linq;
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services.Services;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class OrdersTestsFinalizeOrder
    {
        [Fact]
        public void CheckCorrectGetSupplierName()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testFinalizeOrder");
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

            var deliveryNdeedProduct1 = new DeliveryNeededProduct
            {
                ProductId = 1001,
                Product = "Product1001",
                Supplier = "SomeSupplier1",
                OrderDays = 10,
                DeliveryDays = 10,
                TotalDeliveryTime = 20,
                ProductsAvailable = 1,
                SalesBasedOnDeliveryPeriod = 10,
                ConfimBeenNoticed = true,
            };

            var deliveryNdeedProduct2 = new DeliveryNeededProduct
            {
                ProductId = 1002,
                Product = "Product1002",
                Supplier = "SomeSupplier2",
                OrderDays = 10,
                DeliveryDays = 10,
                TotalDeliveryTime = 20,
                ProductsAvailable = 1,
                SalesBasedOnDeliveryPeriod = 10,
                ConfimBeenNoticed = true,
            };

            dbContext.DeliveryNeededProducts.Add(deliveryNdeedProduct1);
            dbContext.DeliveryNeededProducts.Add(deliveryNdeedProduct2);
            dbContext.SaveChanges();

            var result = order.FinalizeOrder();

            Assert.True(result.IsCompleted);
            Assert.True(result.IsCompletedSuccessfully);

            var list = dbContext.DeliveryNeededProducts.Select(x => x).ToList();

            Assert.Equal("SomeSupplier2", list[0].Supplier);
            Assert.Equal("Product1002", list[0].Product);
        }
    }
}
