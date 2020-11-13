using System;
using System.Linq;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Sale;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService salesService;

        public SalesController(ISalesService salesService)
        {
            this.salesService = salesService;
        }

        public IActionResult All()
        {
            Console.WriteLine("all get");

            var viewModel = salesService.ListOfAllSales();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult All(string customer, string product)
        {
            var viewModel = salesService.ListOfAllSales();

            if (!string.IsNullOrEmpty(customer) && string.IsNullOrEmpty(product))
            {
                viewModel = viewModel.Where(m => m.Customer.ToLower().Contains(customer.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(product) && string.IsNullOrEmpty(customer))
            {
                viewModel = viewModel.Where(m => m.Product.ToLower().Contains(product.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(customer) && !string.IsNullOrEmpty(product))
            {
                viewModel = viewModel.Where(m => m.Customer.ToLower().Contains(customer.ToLower()) && m.Product.ToLower().Contains(product.ToLower())).ToList();
            }


            return this.View(viewModel);
        }
    }
}
