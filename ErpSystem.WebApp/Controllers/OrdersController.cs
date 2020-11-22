using System;
using System.Linq;
using System.Threading.Tasks;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult GenerateOrder()
        {
            var viewModel = ordersService.ProductsForOrderList();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult GenerateOrder(string supplierName)
        {
            var viewModel = ordersService.ProductsForOrderList();

            if (!string.IsNullOrEmpty(supplierName))
            {
                viewModel = viewModel.Where(x => x.Supplier == supplierName);
                return this.View(viewModel);
            }

            return this.View(viewModel);
        }
    }
}
