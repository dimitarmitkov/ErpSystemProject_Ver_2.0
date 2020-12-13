using System;
using System.Linq;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Order;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ErpSystem.Tests.Orders
{
    public class OrdersTestsGenerateOrder
    {

        [Fact]
        public void CheckCorrectGenerateOrderUsingCalculateNeedOfOrderViewModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testGenerateOrder");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var order = new OrdersService(dbContext);

            //var createOrder = new Order
            //{
            //    SupplierId = 100,
            //    Supplier = "First Supplier",
            //    ProductId = 1001,
            //    ProductName = "Product 1001",
            //    OrderDate = DateTime.UtcNow.Date,
            //    NumberOfTransportPackageUnitsOrdered = 10,
            //    TotalAmountOfOrder = 1001.99m,
            //    TotalOrderWeight = 230.23,
            //};
            //dbContext.Orders.Add(createOrder);
            //dbContext.SaveChanges();

            var orderVM = new CalculateNeedOfOrderViewModel
            {
                ProductId = 1001,
                Product = "Product1001",
                ProductMeasurementType = "kg",
                Supplier = "Supplier 1001",
                OrderDays = 10,
                DeliveryDays = 10,
                TotalDeliveryTime = 20,
                ProductsAvailable = 100,
                SalesBasedOnDeliveryPeriod = 1000,
                NumberOfProductsInTrasportUnit = 10,
                NumberOfTransportUnits = 10,
                ProductExwPrice = 10,
                TotalProductPrice = 11.11m,
                TotalWeightOfTransportUnit = 100,
                ConfimBeenNoticed = true,
                NumberOfTransportUnitsClaculatedForOrder = 1,
                OrderedTarnsportUnits = 1,
                Package = "box",
            };

            var deliveryNeed1 = new DeliveryNeededProduct
            {
                Supplier = "First Supplier",
                ProductId = 1001,
                Product = "Product 1001",
                ProductsAvailable = 100,
                SalesBasedOnDeliveryPeriod = 1000,
                DeliveryDays = 10,
                OrderDays = 10,
                TotalDeliveryTime = 20,
                ConfimBeenNoticed = true,
            };

            dbContext.DeliveryNeededProducts.Add(deliveryNeed1);

            var deliveryNeed2 = new DeliveryNeededProduct
            {
                Supplier = "First Supplier",
                ProductId = 1002,
                Product = "Product 10012",
                ProductsAvailable = 200,
                SalesBasedOnDeliveryPeriod = 2000,
                DeliveryDays = 20,
                OrderDays = 20,
                TotalDeliveryTime = 40,
                ConfimBeenNoticed = true,
            };

            dbContext.DeliveryNeededProducts.Add(deliveryNeed2);
            dbContext.SaveChanges();

            order.GenetareOrder(orderVM);

            var result = dbContext.Orders.Select(x => x).ToList();
            var orderedProductName = result[0].ProductName;
            var totalAmountOfOrder = result[0].TotalAmountOfOrder;
            var totalOrderWeight = result[0].TotalOrderWeight;

            Assert.Equal("Product1001", orderedProductName);
            Assert.Equal(10.00m, totalAmountOfOrder, 2);
            Assert.Equal(100.00m, (decimal)totalOrderWeight, 2);
            Assert.True(result.Count() == 1);

            var resultDeliveryNeedProducts = dbContext.DeliveryNeededProducts.Select(x => x);
            var count = resultDeliveryNeedProducts.Count();

            Assert.True(count == 0);
        }
    }
}
