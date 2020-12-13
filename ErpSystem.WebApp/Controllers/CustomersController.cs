﻿using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomersService customersService;

        public CustomersController(ICustomersService customersService)
        {
            this.customersService = customersService;
        }

        public IActionResult CreateCustomer()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(CustomerViewModel customerView)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            customersService.CreateCustomer(customerView);
            return this.Redirect("/Home/Index");
        }


    }
}
