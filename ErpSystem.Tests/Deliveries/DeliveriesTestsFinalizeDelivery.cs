using System;
using System.Linq;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Delivery;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ErpSystem.Tests.Deliveries
{
    public class DeliveriesTestsFinalizeDelivery
    {
        [Fact]
        public void CheckCorrectFinalizeDelivery()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testFinalizeDelivery");
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
                ProductsAvailable = 500,
                ProductionDate = productProduct1002.ProductionDate,
                ExpireDate = productProduct1002.ExpireDate,
                WarehouseId = 1,
            };

            var productFoeWarehouseProduct3 = new WarehouseProduct
            {
                Product = productProduct1003,
                ProductId = productProduct1003.Id,
                ProductsAvailable = 5,
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

            var deliveryList1001 = new DeliveryListViewModel
            {
                NumberOfProductsInTrasportUnit = productProduct1001.ProductTransportPackageNumberOfPieces,
                ProductId = productProduct1001.Id,
                ProductionDate = productProduct1001.ProductionDate,
                ExpireDate = productProduct1001.ExpireDate,
                NumberOfTransportUnits = createOrder1.NumberOfTransportPackageUnitsOrdered,
            };

            var deliveryList1002 = new DeliveryListViewModel
            {
                NumberOfProductsInTrasportUnit = productProduct1002.ProductTransportPackageNumberOfPieces,
                ProductId = productProduct1002.Id,
                ProductionDate = productProduct1002.ProductionDate,
                ExpireDate = productProduct1002.ExpireDate,
                NumberOfTransportUnits = createOrder2.NumberOfTransportPackageUnitsOrdered,
            };

            var deliveryList1003 = new DeliveryListViewModel
            {
                NumberOfProductsInTrasportUnit = productProduct1003.ProductTransportPackageNumberOfPieces,
                ProductId = productProduct1003.Id,
                ProductionDate = productProduct1003.ProductionDate,
                ExpireDate = productProduct1003.ExpireDate,
                NumberOfTransportUnits = createOrder3.NumberOfTransportPackageUnitsOrdered,
            };

            delivery.FinalizeDelivery(deliveryList1001);
            delivery.FinalizeDelivery(deliveryList1002);
            delivery.FinalizeDelivery(deliveryList1003);

            var ordersResultProduct = dbContext.Orders.Select(x => x).ToList();
            var warehouseResult = dbContext.Warehouses.Select(x => x).FirstOrDefault();
            var warehousProductseResult = dbContext.WarehouseProducts.Select(x => x).ToList();
            var finalizedOrderPorduct = dbContext.FinalizedOrders.Select(x => x).ToList();

            Assert.True(ordersResultProduct.Count == 0);
            Assert.False(ordersResultProduct.Where(x => x.ProductName == "Product 1001").Count() == 1);
            Assert.False(ordersResultProduct.Where(x => x.ProductName == "Product 1002").Count() == 1);
            Assert.False(ordersResultProduct.Where(x => x.ProductName == "Product 1003").Count() == 1);
            Assert.True(warehouseResult.CurrentBoxesFrontSpaceFree == 100);
            Assert.True(warehouseResult.CurrentPalletsSpaceFree == 5);
            Assert.True(warehousProductseResult.Where(p => p.ProductId == deliveryList1001.ProductId).FirstOrDefault().ProductsAvailable == 100);
            Assert.True(warehousProductseResult.Where(p => p.ProductId == deliveryList1002.ProductId).FirstOrDefault().ProductsAvailable == 101);
            Assert.True(warehousProductseResult.Where(p => p.ProductId == deliveryList1003.ProductId).FirstOrDefault().ProductsAvailable == 200);
            Assert.True(finalizedOrderPorduct[0].ProductName == "Product 1001");
            Assert.True(finalizedOrderPorduct[1].ProductName == "Product 1002");
            Assert.True(finalizedOrderPorduct[2].ProductName == "Product 1003");
            Assert.True(finalizedOrderPorduct.Count() == 3);
        }
    }
}
