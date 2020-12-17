namespace ErpSystem.Tests.Orders
{
    using System;
    using System.Linq;
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services.Services;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class OrdersTestsProductsForOrderList
    {
        [Fact]
        public void CheckCorrectProductsForOrderList()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testProductsForOrderList");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var order = new OrdersService(dbContext);

            var supplier = new Supplier
            {
                SupplierName = "Supplier1",
                SupplierAddress = "Sofia",
                SupplierCountry = "Bulgaria",
                SupplierPostalCode = "1000",
                CustomsAuthorisationNeeded = false,
                Email = "email@email.com",
                PhoneNumber = "359 887 123 456",
                SupplierAdditionalInformation = "empty",
            };

            dbContext.Suppliers.Add(supplier);
            dbContext.SaveChanges();

            var deliveryNeed1 = new DeliveryNeededProduct
            {
                Supplier = "Supplier1",
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
                Supplier = "Supplier1",
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
                ProductIndentificationNumber = "1001",
                ProductName = "Product 1001",
                ProductLandedPrice = 10,
                ProductSalePrice = 11.11m,
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

            dbContext.Products.Add(productProduct1001);

            var productProduct1002 = new Product
            {
                ProductIndentificationNumber = "1002",
                ProductName = "Product 1002",
                ProductLandedPrice = 10,
                ProductSalePrice = 5.99m,
                ProductGrossMargin = 10,
                ProductDiscount = 10,
                Supplier = dbContext.Suppliers.FirstOrDefault(),
                TimeToOrder = 20,
                TimeToDelivery = 20,
                ProductionDate = DateTime.UtcNow.AddMonths(-2),
                ExpireDate = DateTime.UtcNow,
                ProductTransportPackage = dbContext.TransportPackageTags.FirstOrDefault(),
                MeasurmentTag = dbContext.ProductMeasurmentTags.FirstOrDefault(),
                IsPallet = true,
                ProductTransportPackageWidthSize = 10,
                ProductTransportPackageLengthSize = 10,
                ProductTransportPackageHeightSize = 10,
                ProductTransportPackageWeight = 20,
                ProductTransportPackageNumberOfPieces = 20,
                BoxesPerPallet = 10,
                SingleProductSize = "10",
                ProductDescription = "data",
                IsDeleted = false,
            };

            dbContext.Products.Add(productProduct1002);
            dbContext.SaveChanges();

            var companyType = new CompanyTypeTag
            {
                CompanyTypeOfRegistration = "OOD",
            };

            dbContext.CompanyTypeTags.Add(companyType);
            dbContext.SaveChanges();

            var customer = new Customer
            {
                CompanyName = "FistCustomer",
            };
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            var customerId = dbContext.Customers.Select(x => x.Id).FirstOrDefault();

            var sale1 = new Sale
            {
                ProductId = 1,
                CustomerId = customerId,
                Customer = dbContext.Customers.FirstOrDefault(c => c.Id == customerId),
                Product = dbContext.Products.FirstOrDefault(p => p.Id == 1001),
                SaleDate = DateTime.UtcNow.AddDays(-2),
                NumberOfSoldProducts = 10,
                HasCustomerDiscount = true,
                HasProductDiscount = true,
            };

            dbContext.Sales.Add(sale1);

            var sale2 = new Sale
            {
                ProductId = 2,
                CustomerId = customerId,
                Customer = dbContext.Customers.FirstOrDefault(c => c.Id == customerId),
                Product = dbContext.Products.FirstOrDefault(p => p.Id == 1001),
                SaleDate = DateTime.UtcNow.AddDays(-3),
                NumberOfSoldProducts = 10,
                HasCustomerDiscount = true,
                HasProductDiscount = true,
            };

            dbContext.Sales.Add(sale2);
            dbContext.SaveChanges();

            var productFoeWarehouseProduct1 = new WarehouseProduct
            {
                Product = productProduct1001,
                ProductId = productProduct1001.Id,
                ProductsAvailable = 1,
                ProductionDate = productProduct1001.ProductionDate,
                ExpireDate = productProduct1001.ExpireDate,
                WarehouseId = 1,
            };

            var productFoeWarehouseProduct2 = new WarehouseProduct
            {
                Product = productProduct1002,
                ProductId = productProduct1002.Id,
                ProductsAvailable = 1000,
                ProductionDate = productProduct1002.ProductionDate,
                ExpireDate = productProduct1002.ExpireDate,
                WarehouseId = 1,
            };

            dbContext.WarehouseProducts.Add(productFoeWarehouseProduct1);
            dbContext.WarehouseProducts.Add(productFoeWarehouseProduct2);
            dbContext.SaveChanges();

            var result = order.ProductsForOrderList().ToList();

            Assert.Equal(1m, result.Count(), 0);
            Assert.True(result[0].Product == "Product 1001");
            Assert.True(result[0].NumberOfTransportUnitsClaculatedForOrder == 1);
        }
    }
}
