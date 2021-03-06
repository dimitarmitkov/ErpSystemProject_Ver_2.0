﻿namespace ErpSystem.Tests
{
    using System;
    using System.Linq;
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services.Services;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ProductTestsSearchByProductSupplierCountryOrCity
    {
        [Fact]
        public void CheckCorrectSearchByProductSupplierCountryOrCityResult()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testCountryOrCity");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var product = new ProductsService(dbContext);

            var supplier1 = new Supplier
            {
                SupplierName = "Supplier1",
                SupplierAddress = "Sofia",
                SupplierCountry = "Bulgaria",
                SupplierPostalCode = "1000",
                CustomsAuthorisationNeeded = false,
                Email = "firstSupplier@email.com",
                PhoneNumber = "359 887 111 111",
                SupplierAdditionalInformation = "empty",
            };

            dbContext.Suppliers.Add(supplier1);

            var supplier2 = new Supplier
            {
                SupplierName = "Supplier2",
                SupplierAddress = "Berlin",
                SupplierCountry = "Germany",
                SupplierPostalCode = "10020",
                CustomsAuthorisationNeeded = true,
                Email = "secondSupplier@email.com",
                PhoneNumber = "359 887 222 222",
                SupplierAdditionalInformation = "empty",
            };
            dbContext.Suppliers.Add(supplier2);
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

            var productProduct1 = new Product
            {
                ProductIndentificationNumber = "1234",
                ProductName = "First",
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

            var productProduct2 = new Product
            {
                ProductIndentificationNumber = "1234",
                ProductName = "Second",
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
                ProductTransportPackageLengthSize = 10,
                ProductTransportPackageHeightSize = 10,
                ProductTransportPackageWeight = 10,
                ProductTransportPackageNumberOfPieces = 10,
                BoxesPerPallet = 10,
                SingleProductSize = "10",
                ProductDescription = "data",
                IsDeleted = false,
            };

            dbContext.Products.Add(productProduct1);
            dbContext.Products.Add(productProduct2);
            dbContext.SaveChanges();

            var productFoeWarehouseProduct1 = new WarehouseProduct
            {
                Product = productProduct1,
                ProductId = productProduct1.Id,
                ProductsAvailable = 10,
                ProductionDate = productProduct1.ProductionDate,
                ExpireDate = productProduct1.ExpireDate,
                WarehouseId = 1,
            };

            var productFoeWarehouseProduct2 = new WarehouseProduct
            {
                Product = productProduct2,
                ProductId = productProduct2.Id,
                ProductsAvailable = 100,
                ProductionDate = productProduct2.ProductionDate,
                ExpireDate = productProduct2.ExpireDate,
                WarehouseId = 1,
            };

            dbContext.WarehouseProducts.Add(productFoeWarehouseProduct1);
            dbContext.WarehouseProducts.Add(productFoeWarehouseProduct2);
            dbContext.SaveChanges();

            var all = product.SearchByProductSupplierCountryOrCity("Bulgaria", "Sofia").ToList();

            Assert.Equal("Supplier1", all[0].Supplier);
            Assert.Equal(1m, all.Count, 0);

            all = product.SearchByProductSupplierCountryOrCity("Germany", "Berlin").ToList();

            Assert.Equal("Supplier2", all[0].Supplier);
            Assert.Equal(1m, all.Count, 0);

            all = product.SearchByProductSupplierCountryOrCity(null, null).ToList();

            Assert.Equal("Supplier2", all[0].Supplier);
            Assert.Equal("Supplier1", all[1].Supplier);
            Assert.Equal(2m, all.Count, 0);

            all = product.SearchByProductSupplierCountryOrCity("Germany", null).ToList();

            Assert.Equal("Supplier2", all[0].Supplier);

            all = product.SearchByProductSupplierCountryOrCity(null, "Berlin").ToList();

            Assert.Equal("Supplier2", all[0].Supplier);
            Assert.Equal(1m, all.Count, 0);

            all = product.SearchByProductSupplierCountryOrCity(null, "Sofia").ToList();

            Assert.Equal("Supplier1", all[0].Supplier);
            Assert.Equal(1m, all.Count, 0);

            all = product.SearchByProductSupplierCountryOrCity("Bulgaria", null).ToList();

            Assert.Equal("Supplier1", all[0].Supplier);
            Assert.Equal(1m, all.Count, 0);
        }
    }
}
