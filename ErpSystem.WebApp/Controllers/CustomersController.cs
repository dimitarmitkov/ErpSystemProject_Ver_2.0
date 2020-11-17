using System;
using System.Threading.Tasks;
using ErpSystem.Services.Services;
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




    }
}
