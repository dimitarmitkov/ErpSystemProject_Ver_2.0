using System;
using System.Linq;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace ErpSystem.Tests.Orders
{
    public class OrdersTestsSelectSupplier
    {
        [Fact]
        public void CheckCorrectSelectSupplier()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testSelectSupplier");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var order = new OrdersService(dbContext);

            var supplier1 = new Supplier
            {
                SupplierName = "Supplier1",
                SupplierAddress = "Sofia",
                SupplierCountry = "Bulgaria",
                SupplierPostalCode = "1000",
                CustomsAuthorisationNeeded = false,
                Email = "email1@email.com",
                PhoneNumber = "359 887 123 456",
                SupplierAdditionalInformation = "empty"
            };

            dbContext.Suppliers.Add(supplier1);

            var supplier2 = new Supplier
            {
                SupplierName = "Supplier2",
                SupplierAddress = "Plovdiv",
                SupplierCountry = "Bulgaria",
                SupplierPostalCode = "4000",
                CustomsAuthorisationNeeded = false,
                Email = "email2@email.com",
                PhoneNumber = "359 887 123 456",
                SupplierAdditionalInformation = "empty"
            };

            dbContext.Suppliers.Add(supplier2);
            dbContext.SaveChanges();

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
            dbContext.SaveChanges();
            order.SelectSupplier("Supplier1");

            var result = dbContext.SupplierForOrders.Select(x => x).ToList();

            Assert.Equal(1m, result.Count(), 0);
            Assert.True(result[0].SupplierName == "Supplier1");
        }
    }
}
