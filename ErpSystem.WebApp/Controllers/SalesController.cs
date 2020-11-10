using System;
using ErpSystem.Services.Services;
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
            var viewModel = salesService.ListOfSales();

            return this.View(viewModel);
        }
    }
}
