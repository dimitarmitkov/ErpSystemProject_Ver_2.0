using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Sale;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{

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
    }
}
