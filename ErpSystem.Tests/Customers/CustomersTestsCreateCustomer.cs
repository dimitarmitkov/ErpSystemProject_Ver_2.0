using System.Linq;
using AutoMapper;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Customer;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ErpSystem.Tests.Customers
{
    public class CustomersTestsCreateCustomer
    {
        [Fact]
        public void CheckCorrectCreateCustomerResult()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testFindAllProducts");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
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

            var createCustomer = new Customer
            {
                CompanyName = "Company1",
                City = "Sofia",
                Address = "Sofia Address",
                PostalCode = 1000,
                PhoneNumber = "359 888 111 111",
                CustomerDiscount = 10,
                IsActive = true,
                HasDelivery = true,
                Email = "customer1@mail.com",
                CompanyTypeOfRegistration = dbContext.CompanyTypeTags.FirstOrDefault(),
            };

            dbContext.Customers.Add(createCustomer);
            dbContext.SaveChanges();

            var createCustomerVM = new CustomerViewModel
            {
                CompanyName = "Company1",
                City = "Sofia",
                Address = "Sofia Address",
                PostalCode = 1000,
                PhoneNumber = "359 888 111 111",
                CustomerDiscount = 10,
                IsActive = true,
                HasDelivery = true,
                Email = "customer1@mail.com",
                CompanyType = dbContext.CompanyTypeTags.FirstOrDefault().ToString(),
            };

            var result = customer.CreateCustomer(createCustomerVM);

            Assert.True(result.IsCompleted);
            Assert.True(result.IsCompletedSuccessfully);
            Assert.False(result.IsFaulted);
        }
    }
}