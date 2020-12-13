using System;
using System.Threading.Tasks;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ErpSystem.Tests
{
    public class ProductsTestsCreateProduct
    {
        [Fact]
        public void CheckCorrectCreateProductUsingCreateProductViewModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testCreateProduct");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var product = new ProductsService(dbContext);

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

            Task result = product.CreateProduct(productVM);
            Assert.True(result.IsCompleted);
            Assert.True(result.IsCompletedSuccessfully);
            Assert.False(result.IsFaulted);
        }
    }
}