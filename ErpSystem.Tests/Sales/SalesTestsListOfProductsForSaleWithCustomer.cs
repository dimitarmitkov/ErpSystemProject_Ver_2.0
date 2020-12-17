namespace ErpSystem.Tests
{
    using System;
    using System.Linq;
    using AutoMapper;
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services;
    using ErpSystem.Services.Services;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class SalesTestsListOfProductsForSaleWithCustomer
    {
        [Fact]
        public void CheckCorrectListOfProductsForSaleWithCustomer()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testListOfProductsForSaleWithCustomer");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var sale = new SalesService(dbContext);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            var mapper = mockMapper.CreateMapper();
            var customer = new CustomersService(dbContext, mapper);

            var companyTypeTag = new CompanyTypeTag
            {
                CompanyTypeOfRegistration = "OOD",
            };

            dbContext.CompanyTypeTags.Add(companyTypeTag);
            dbContext.SaveChanges();

            var createCustomer1 = new Customer
            {
                CompanyName = "Company1",
                City = "Sofia",
                Address = "Sofia Address",
                PostalCode = 1000,
                PhoneNumber = "359 888 111 111",
                CustomerDiscount = 20,
                IsActive = true,
                HasDelivery = true,
                Email = "customer1@mail.com",
                CompanyTypeOfRegistration = dbContext.CompanyTypeTags.FirstOrDefault(),
                CompanyEik = "BG111111111",
            };

            dbContext.Customers.Add(createCustomer1);

            var createCustomer2 = new Customer
            {
                CompanyName = "Company2",
                City = "Sofia",
                Address = "Sofia Address",
                PostalCode = 1000,
                PhoneNumber = "359 888 111 111",
                CustomerDiscount = 20,
                IsActive = true,
                HasDelivery = true,
                Email = "customer1@mail.com",
                CompanyTypeOfRegistration = dbContext.CompanyTypeTags.FirstOrDefault(),
                CompanyEik = "BG222222222",
            };

            dbContext.Customers.Add(createCustomer2);
            dbContext.SaveChanges();

            var currenSaleCustomer = new CurrentSale
            {
                CustomerEikNumber = createCustomer1.CompanyEik,
                CustomerId = dbContext.Customers.Where(x => x.CompanyEik == createCustomer1.CompanyEik).Select(y => y.Id).FirstOrDefault(),
                CustomerName = createCustomer1.CompanyName,
                HasCustomerDiscount = false,
            };

            dbContext.CurrentSales.Add(currenSaleCustomer);
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
                ProductIndentificationNumber = "1234",
                ProductName = "Product 1001",
                ProductLandedPrice = 100,
                ProductSalePrice = 100m,
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
                ProductDiscount = 7,
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
                ProductSalePrice = 9.99m,
                ProductGrossMargin = 10,
                ProductDiscount = 5,
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

            var productFoeWarehouseProduct1 = new WarehouseProduct
            {
                Product = productProduct1001,
                ProductId = productProduct1001.Id,
                ProductsAvailable = 0,
                ProductionDate = productProduct1001.ProductionDate,
                ExpireDate = productProduct1001.ExpireDate,
                WarehouseId = 1,
            };

            var productFoeWarehouseProduct2 = new WarehouseProduct
            {
                Product = productProduct1002,
                ProductId = productProduct1002.Id,
                ProductsAvailable = 500,
                ProductionDate = productProduct1002.ProductionDate,
                ExpireDate = productProduct1002.ExpireDate,
                WarehouseId = 1,
            };

            var productFoeWarehouseProduct3 = new WarehouseProduct
            {
                Product = productProduct1003,
                ProductId = productProduct1003.Id,
                ProductsAvailable = 459,
                ProductionDate = productProduct1003.ProductionDate,
                ExpireDate = productProduct1003.ExpireDate,
                WarehouseId = 1,
            };

            dbContext.WarehouseProducts.Add(productFoeWarehouseProduct1);
            dbContext.WarehouseProducts.Add(productFoeWarehouseProduct2);
            dbContext.WarehouseProducts.Add(productFoeWarehouseProduct3);
            dbContext.SaveChanges();

            var result = sale.ListOfProductsForSaleWithCustomer().ToList();

            Assert.True(result.Count() == 2);
            Assert.True(result.Sum(x => x.ProductsAvailable) == 959);
            Assert.True(result[0].ProductMeasurement == "kg");
            Assert.True(result[1].ProductMeasurement == "kg");
        }
    }
}
