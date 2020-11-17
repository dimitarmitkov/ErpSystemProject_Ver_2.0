using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ErpSystemDbContext dbContext;

        public CustomersService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // create customer service
        public void CreateCustomer(CustomerViewModel customerView)
        {
            var customer = new Customer
            {
                CompanyName = customerView.CompanyName,
                City = customerView.City,
                Address = customerView.Address,
                PostalCode = customerView.PostalCode,
                PhoneNumber = customerView.PhoneNumber,
                CustomerDiscount = customerView.CustomerDiscount,
                IsActive = customerView.IsActive,
                HasDelivery = customerView.HasDelivery,
                Email = customerView.Email,
                CustomerAdditionalInfo = customerView.AdditionalInfo,
            };

            var customerTypeEntity = this.dbContext.CompanyTypeTags.FirstOrDefault(c => c.CompanyTypeOfRegistration == customerView.CompanyType);

            if (customerTypeEntity == null)
            {
                customerTypeEntity = new CompanyTypeTag
                {
                    CompanyTypeOfRegistration = customerView.CompanyType,
                };
            }

            customer.CompanyTypeOfRegistration = customerTypeEntity;

            this.dbContext.Customers.Add(customer);
            this.dbContext.SaveChanges();
        }

        public void DeleteCustomer(string id, string companyName)
        {
            IEnumerable<Customer> customerToDeleteList = null;

            if (!string.IsNullOrEmpty(id))
            {
                customerToDeleteList = this.dbContext.Customers.Where(c => c.Id == id);
            }
            else if (!string.IsNullOrEmpty(companyName))
            {
                customerToDeleteList = this.dbContext.Customers.Where(c => c.CompanyName == companyName);
            }

            foreach (var customerToDelete in customerToDeleteList)
            {
                this.dbContext.Customers.Remove(customerToDelete);
            }
            this.dbContext.SaveChanges();
        }

        public IEnumerable<CustomerViewModel> SearchByCustomerNamePostalCodeAndAddress(string customerName, int? customerPostalCode, string customerAddress)
        {
            if (!string.IsNullOrWhiteSpace(customerName))
            {
                IQueryable<Customer> customerView = this.dbContext.Customers.Where(c => c.CompanyName == customerName);

                return SelectCustomerViewModel(customerView);
            }
            else if (!string.IsNullOrWhiteSpace(customerPostalCode.ToString()))
            {
                IQueryable<Customer> customerView = this.dbContext.Customers.Where(c => c.PostalCode == customerPostalCode);

                return SelectCustomerViewModel(customerView);
            }
            else if (!string.IsNullOrWhiteSpace(customerAddress))
            {
                IQueryable<Customer> customerView = this.dbContext.Customers.Where(c => c.Address == customerAddress);

                return SelectCustomerViewModel(customerView);
            }

            else
            {
                IQueryable<Customer> customerView = this.dbContext.Customers.Where(c => c.Id != null);

                return SelectCustomerViewModel(customerView);
            }
        }


        public IEnumerable<CustomerViewModel> SearchByCustomerPhoneAndEmail(string customerPhone, string customerEmail)
        {
            if (!string.IsNullOrWhiteSpace(customerPhone))
            {
                IQueryable<Customer> customerView = this.dbContext.Customers.Where(c => c.PhoneNumber == customerPhone);

                return SelectCustomerViewModel(customerView);
            }

            else if (!string.IsNullOrWhiteSpace(customerEmail))
            {
                IQueryable<Customer> customerView = this.dbContext.Customers.Where(c => c.Email == customerEmail);

                return SelectCustomerViewModel(customerView);
            }

            else
            {
                IQueryable<Customer> customerView = this.dbContext.Customers.Where(c => c.Id != null);

                return SelectCustomerViewModel(customerView);
            }
        }

        //public IEnumerable<SelectListItem> SeclectCustomerDropDown()
        //{
        //    return this.dbContext.Customers.Select(p => new SelectListItem
        //    {
        //        Text = p.CompanyName,
        //        Value = p.CompanyName,

        //    }).ToList();
        //}

        private static IEnumerable<CustomerViewModel> SelectCustomerViewModel(IQueryable<Customer> customerView)
        {
            return customerView.Select(x => new CustomerViewModel
            {
                CompanyName = x.CompanyName,
                CompanyType = x.CompanyTypeOfRegistration.CompanyTypeOfRegistration,
                City = x.City,
                Address = x.Address,
                IsActive = x.IsActive,
                Email = x.Email,
                CustomerDiscount = x.CustomerDiscount,
                PostalCode = x.PostalCode,
                PhoneNumber = x.PhoneNumber,
                HasDelivery = x.HasDelivery,
                AdditionalInfo = x.CustomerAdditionalInfo,
            }).ToList();
        }
    }
}
