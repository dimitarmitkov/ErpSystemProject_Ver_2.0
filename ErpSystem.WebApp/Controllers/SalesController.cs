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
            Console.WriteLine("all post");

            var viewModel = salesService.ListOfAllSales();

            if (!string.IsNullOrEmpty(customer))
            {
                viewModel = viewModel.Where(m => m.Customer.Contains(customer)).ToList();
            }

            if (!string.IsNullOrEmpty(product))
            {
                viewModel = viewModel.Where(m => m.Product.Contains(product)).ToList();
            }

            if (!string.IsNullOrEmpty(customer) && !string.IsNullOrEmpty(product))
            {
                viewModel = viewModel.Where(m => m.Customer.Contains(customer) && m.Product.Contains(product)).ToList();
            }

            return this.View(viewModel);
        }
    }
}
