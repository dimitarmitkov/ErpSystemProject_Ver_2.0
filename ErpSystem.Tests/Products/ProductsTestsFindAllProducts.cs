using Microsoft.EntityFrameworkCore;
using Xunit;
using ErpSystem.Data;
using ErpSystem.Models;
using System;
using ErpSystem.Services.ViewModels.Product;
using ErpSystem.Services.Services;
using System.Linq;

namespace ErpSystem.Tests
{
    public class ProductsTestsFindAllProducts
    {
        [Fact]
        public void CheckCorrectFindAllProductsResult()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testFindAll");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);

            var productVM = new CreateProductViewModel
            {
                ProductIndentificationNumber = "1234",
                ProductName = "First",
                ProductLendedPrice = 10,
                ProductGrossMargin = 10,
                ProductDiscount = 10,
                Supplier = "Supplier",
                TimeToOrder = 10,
                TimeToDelivery = 10,
                ProductionDate = DateTime.UtcNow.AddMonths(-2).ToString(),
                ExpireDate = DateTime.UtcNow.ToString(),
                ProductTransportPackage = "box",
                MeasurmentTag = "kg",
                IsPallet = "false",
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

            var supplier = new Supplier
            {
                SupplierName = "Supplier1",
                SupplierAddress = "Sofia",
                SupplierCountry = "Bulgaria",
                SupplierPostalCode = "1000",
                CustomsAuthorisationNeeded = false,
                Email = "email@email.com",
                PhoneNumber = "359 887 123 456",
                SupplierAdditionalInformation = "empty"
            };

            dbContext.Suppliers.Add(supplier);
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

            var productProduct = new Product
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

            var product = new ProductsService(dbContext);

            dbContext.Products.Add(productProduct);
            dbContext.SaveChanges();

            var productFoeWarehouseProduct = new WarehouseProduct
            {
                Product = productProduct,
                ProductId = productProduct.Id,
                ProductsAvailable = 10,
                ProductionDate = productProduct.ProductionDate,
                ExpireDate = productProduct.ExpireDate,
                WarehouseId = 1,
            };

            dbContext.WarehouseProducts.Add(productFoeWarehouseProduct);
            dbContext.SaveChanges();

            var all = product.FindAll().ToList();

            Assert.Equal(11.11m, all[0].ProductSalePrice, 2);
            Assert.Equal("First", all[0].ProductName);
            Assert.Equal(1m, all.Count, 0);
        }
    }
}