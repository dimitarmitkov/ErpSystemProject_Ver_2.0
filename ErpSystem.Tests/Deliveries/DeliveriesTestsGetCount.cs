namespace ErpSystem.Tests.Deliveries
{
    using System;
    using System.Linq;
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services.Services;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class DeliveriesTestsGetCount
    {
        [Fact]
        public void CheckCorrectGetCounty()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("tesGetCount");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var delivery = new DeliveriesService(dbContext);

            var transportPackage = new TransportPackageTag
            {
                TypeOfPackage = "box",
            };

            dbContext.TransportPackageTags.Add(transportPackage);
            dbContext.SaveChanges();

            var measurementTag = new ProductMeasurmentTag
            {
                Maesurment = "kg",
            };

            dbContext.ProductMeasurmentTags.Add(measurementTag);
            dbContext.SaveChanges();

            var productProduct1001 = new Product
            {
                ProductIndentificationNumber = "1234",
                ProductName = "Product 1001",
                ProductLandedPrice = 100,
                ProductSalePrice = 111.1m,
                ProductGrossMargin = 10,
                ProductDiscount = 10,
                Supplier = dbContext.Suppliers.FirstOrDefault(),
                TimeToOrder = 10,
                TimeToDelivery = 10,
                ProductionDate = DateTime.UtcNow.AddMonths(-2),
                ExpireDate = DateTime.UtcNow,
                ProductTransportPackage = dbContext.TransportPackageTags.FirstOrDefault(),
                MeasurmentTag = dbContext.ProductMeasurmentTags.FirstOrDefault(),
                IsPallet = false,
                ProductTransportPackageWidthSize = 10,
                ProductTransportPackageLengthSize = 10,
                ProductTransportPackageHeightSize = 10,
                ProductTransportPackageWeight = 10,
                ProductTransportPackageNumberOfPieces = 10,
                BoxesPerPallet = 10,
                SingleProductSize = "10",
                ProductDescription = "data",
                IsDeleted = false,
            };

            var productProduct1002 = new Product
            {
                ProductIndentificationNumber = "1234",
                ProductName = "Product 1002",
                ProductLandedPrice = 10,
                ProductSalePrice = 11.11m,
                ProductGrossMargin = 10,
                ProductDiscount = 10,
                Supplier = dbContext.Suppliers.LastOrDefault(),
                TimeToOrder = 10,
                TimeToDelivery = 10,
                ProductionDate = DateTime.UtcNow.AddMonths(-2),
                ExpireDate = DateTime.UtcNow,
                ProductTransportPackage = dbContext.TransportPackageTags.FirstOrDefault(),
                MeasurmentTag = dbContext.ProductMeasurmentTags.FirstOrDefault(),
                IsPallet = false,
                ProductTransportPackageWidthSize = 10,
                ProductTransportPackageLengthSize = 100,
                ProductTransportPackageHeightSize = 10,
                ProductTransportPackageWeight = 10,
                ProductTransportPackageNumberOfPieces = 101,
                BoxesPerPallet = 10,
                SingleProductSize = "10",
                ProductDescription = "data",
                IsDeleted = false,
            };

            var productProduct1003 = new Product
            {
                ProductIndentificationNumber = "1234",
                ProductName = "Product 1003",
                ProductLandedPrice = 10,
                ProductSalePrice = 11.11m,
                ProductGrossMargin = 10,
                ProductDiscount = 10,
                Supplier = dbContext.Suppliers.LastOrDefault(),
                TimeToOrder = 10,
                TimeToDelivery = 10,
                ProductionDate = DateTime.UtcNow.AddMonths(-2),
                ExpireDate = DateTime.UtcNow,
                ProductTransportPackage = dbContext.TransportPackageTags.FirstOrDefault(),
                MeasurmentTag = dbContext.ProductMeasurmentTags.FirstOrDefault(),
                IsPallet = true,
                ProductTransportPackageWidthSize = 10,
                ProductTransportPackageLengthSize = 10,
                ProductTransportPackageHeightSize = 10,
                ProductTransportPackageWeight = 10,
                ProductTransportPackageNumberOfPieces = 10,
                BoxesPerPallet = 10,
                SingleProductSize = "10",
                ProductDescription = "data",
                IsDeleted = false,
            };

            dbContext.Products.Add(productProduct1001);
            dbContext.Products.Add(productProduct1002);
            dbContext.Products.Add(productProduct1003);
            dbContext.SaveChanges();

            var createOrder1 = new Order
            {
                SupplierId = 100,
                Supplier = "Supplier1",
                ProductId = productProduct1001.Id,
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
                ProductId = productProduct1002.Id,
                ProductName = "Product 1002",
                OrderDate = DateTime.UtcNow.Date,
                NumberOfTransportPackageUnitsOrdered = 1,
                TotalAmountOfOrder = 23960m,
                TotalOrderWeight = 80000,
            };
            dbContext.Orders.Add(createOrder2);

            var createOrder3 = new Order
            {
                SupplierId = 200,
                Supplier = "Supplier2",
                ProductId = productProduct1003.Id,
                ProductName = "Product 1003",
                OrderDate = DateTime.UtcNow.Date,
                NumberOfTransportPackageUnitsOrdered = 2,
                TotalAmountOfOrder = 23960m,
                TotalOrderWeight = 80000,
            };
            dbContext.Orders.Add(createOrder3);
            dbContext.SaveChanges();

            var result = delivery.GetCount();

            Assert.True(result == 3);
        }
    }
}
