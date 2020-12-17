namespace ErpSystem.WebApp.Controllers
{
    using ErpSystem.Services.Services;
    using ErpSystem.Services.ViewModels.Sale;
    using Microsoft.AspNetCore.Mvc;

    public class SalesRestController : Controller
    {
        private readonly ISalesService salesService;

        public SalesRestController(ISalesService salesService)
        {
            this.salesService = salesService;
        }

        public IActionResult SaleRest()
        {
            var viewModel = new SaleRestCombinedViewModel
            {
                List = this.salesService.ListOfSales(null, null),
            };

            return this.View(viewModel);
        }

        public IActionResult SaleRestReturn()
        {
            var viewModel = new SaleRestCombinedViewModel
            {
                List = this.salesService.ListOfSales(null, null),
            };

            return this.Json(viewModel);
        }
    }
}
