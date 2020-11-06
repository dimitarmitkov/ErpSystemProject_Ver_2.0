using System;
using System.Collections.Generic;
using ErpSystem.Services.Models;

namespace ErpSystem.Services
{
    public interface ICustomersService
    {
        void CreateCustomer(CustomerViewModel customerView);

        IEnumerable<CustomerViewModel> SearchByCustomerNamePostalCodeAndAddress(string customerName, int? customerPostalCode, string customerAddress);

        IEnumerable<CustomerViewModel> SearchByCustomerPhoneAndEmail(string customerPhone, string customerEmail);

        void DeleteCustomer(string id, string companyName);
    }
}
