using System.Linq;
using AutoMapper;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services;
using ErpSystem.Services.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ErpSystem.Tests.Sales
{
    public class SalesTestsGenerateCurrentSale
    {
        [Fact]
        public void CheckCorrectGenerateCurrentSale()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testGenerateCurrentSale");
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
                CompanyTypeOfRegistration = "OOD"
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
                CompanyEik = "BG111111111"
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
                CompanyEik = "BG222222222"
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

            sale.GenerateCurrentSale(createCustomer1.CompanyEik, true, "abc");

            var customerModel = dbContext.CurrentSales.ToList();

            Assert.True(customerModel[0].CustomerEikNumber == "BG111111111");

            sale.GenerateCurrentSale(createCustomer2.CompanyEik, true, "abc");

            customerModel = dbContext.CurrentSales.ToList();

            Assert.True(customerModel[0].CustomerEikNumber == "BG222222222");
        }
    }
}
