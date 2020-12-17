namespace ErpSystem.Tests.Sales
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

    public class SalesTestsTotalSalesPerDate
    {
        [Fact]
        public void CheckCorrectTotalSalesPerDate()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testTotalSalesPerDate");
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

            var createCustomer = new Customer
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
            };

            dbContext.Customers.Add(createCustomer);
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
                ProductsAvailable = 10,
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

            var warehouseSpaceInsert = new Warehouse
            {
                WarehouseName = "Warehouse1",
                CurrentBoxesFrontSpaceFree = 300,
                CurrentPalletsSpaceFree = 7,
            };

            dbContext.Warehouses.Add(warehouseSpaceInsert);
            dbContext.SaveChanges();

            var warehoseBoxesInsert = new WarehouseBoxSpace
            {
                WarehouseNumber = 1,
                ShelfDepth = 60,
            };

            dbContext.WarehouseBoxes.Add(warehoseBoxesInsert);
            dbContext.SaveChanges();

            var productsFromDb = dbContext.Products.Select(x => x).ToList();
            var cistomersFromDb = dbContext.Customers.Select(x => x).ToList();
            var warehouseProductsFromDb = dbContext.WarehouseProducts.Select(x => x).ToList();
            var warehouseFromDb = dbContext.Warehouses.Select(x => x).FirstOrDefault();

            var currentCusomer = cistomersFromDb[0];
            var firstProduct = productsFromDb[0];
            var secondProduct = productsFromDb[1];
            var thirdProduct = productsFromDb[2];

            sale.CreateSale(firstProduct.Id, currentCusomer.Id, 2, true, true, 1, warehouseProductsFromDb.Where(x => x.ProductId == firstProduct.Id).Select(y => y.ProductId).FirstOrDefault());

            sale.CreateSale(firstProduct.Id, currentCusomer.Id, 8, true, true, 1, warehouseProductsFromDb.Where(x => x.ProductId == firstProduct.Id).Select(y => y.ProductId).FirstOrDefault());

            sale.CreateSale(secondProduct.Id, currentCusomer.Id, 101, true, true, 1, warehouseProductsFromDb.Where(x => x.ProductId == secondProduct.Id).Select(y => y.ProductId).FirstOrDefault());

            sale.CreateSale(thirdProduct.Id, currentCusomer.Id, 158, false, false, 1, warehouseProductsFromDb.Where(x => x.ProductId == thirdProduct.Id).Select(y => y.ProductId).FirstOrDefault());

            sale.CreateSale(thirdProduct.Id, currentCusomer.Id, 2, true, true, 1, warehouseProductsFromDb.Where(x => x.ProductId == thirdProduct.Id).Select(y => y.ProductId).FirstOrDefault());

            var result = sale.TotalSalesPerDate();
            var date = string.Empty;
            var sales = 0m;

            foreach (var res in result)
            {
                date = res.Key;
                sales = res.Value;
            }

            Assert.True(date == DateTime.UtcNow.Date.ToString("yyyy-MM-dd"));
            Assert.Equal(3148.45m, sales, 2);
        }
    }
}
