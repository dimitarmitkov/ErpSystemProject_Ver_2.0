﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.Customer;

namespace ErpSystem.Services.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ErpSystemDbContext dbContext;
        private readonly IMapper mapper;

        public CustomersService(ErpSystemDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // create customer service, test done
        public async Task CreateCustomer(CustomerViewModel customerView)
        {
            var customer = mapper.Map<Customer>(customerView);

            var customerTypeEntity = this.dbContext.CompanyTypeTags.FirstOrDefault(c => c.CompanyTypeOfRegistration == customerView.CompanyType);

            if (customerTypeEntity == null)
            {
                customerTypeEntity = new CompanyTypeTag
                {
                    CompanyTypeOfRegistration = customerView.CompanyType,
                };
            }

            customer.CompanyTypeOfRegistration = customerTypeEntity;

            await this.dbContext.Customers.AddAsync(customer);
            await this.dbContext.SaveChangesAsync();
        }

        // TODO add is deleted in Customer table in DB, then change remove to update and add is deleted = true
        public async Task DeleteCustomer(string id, string companyName)
        {
            IEnumerable<Customer> customerToDeleteList = null;

            if (!string.IsNullOrEmpty(id)) customerToDeleteList = this.dbContext.Customers.Where(c => c.Id == id);
            else if (!string.IsNullOrEmpty(companyName)) customerToDeleteList = this.dbContext.Customers.Where(c => c.CompanyName == companyName);

            foreach (var customerToDelete in customerToDeleteList)
            {
                this.dbContext.Customers.Remove(customerToDelete);
            }
            await this.dbContext.SaveChangesAsync();
        }
    }
}