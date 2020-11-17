using System;
using System.Collections.Generic;
using ErpSystem.Services.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.Services
{
    public interface ICustomersService
    {
        void CreateCustomer(CustomerViewModel customerView);

        IEnumerable<CustomerViewModel> SearchByCustomerNamePostalCodeAndAddress(string customerName, int? customerPostalCode, string customerAddress);

        IEnumerable<CustomerViewModel> SearchByCustomerPhoneAndEmail(string customerPhone, string customerEmail);

        void DeleteCustomer(string id, string companyName);

        //IEnumerable<SelectListItem> SeclectCustomerDropDown();
    }
}
