using System;
using ErpSystem.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ISalesService salesService;

        public ChartsController(ISalesService salesService)
        {
            this.salesService = salesService;
        }

        public IActionResult Chart()
        {
            var viewModel = this.salesService.ListOfSales();

            //return this.Json(viewModel);

            return this.View(viewModel);
        }
    }
}
