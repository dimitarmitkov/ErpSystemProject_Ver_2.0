using System;
using System.Linq;
using System.Threading.Tasks;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
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

            SupplierOrderViewModel viewModel = new SupplierOrderViewModel();
            viewModel.CalculateOrderProductsList = ordersService.ProductsForOrderList();
            viewModel.SuppliersListDropDown = ordersService.SuppliersDropDown();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult GenerateOrder(SupplierOrderViewModel supplierOrder)
        {
            var viewModel = new SupplierOrderViewModel();
            if (!ModelState.IsValid)
            {
                viewModel.CalculateOrderProductsList = ordersService.ProductsForOrderList();
                viewModel.SuppliersListDropDown = ordersService.SuppliersDropDown();

                return this.View(viewModel);
            }

            var supplierName = supplierOrder.Suppliers.Supplier;

            ordersService.SelectSupplier(supplierName);

            viewModel.CalculateOrderProductsList = ordersService.ProductsForOrderList();

            if (!string.IsNullOrEmpty(supplierName))
            {
                viewModel.CalculateOrderProductsList = viewModel.CalculateOrderProductsList.Where(x => x.Supplier == supplierName);
                return this.Redirect("/Orders/ExecuteOrder");
            }

            return this.View(viewModel);
        }

        public IActionResult ExecuteOrder()
        {
            var supplierName = ordersService.GetSupplierName();

            var viewModel = new SupplierOrderViewModel();
            if (!ModelState.IsValid)
            {
                viewModel.CalculateOrderProductsList = ordersService.ProductsForOrderList();
                viewModel.CalculateOrderProductsList = ordersService.ProductsForOrderList();

                return this.View(viewModel);
            }

            viewModel.CalculateOrderProductsList = ordersService.ProductsForOrderList();

            if (!string.IsNullOrEmpty(supplierName))
            {
                viewModel.CalculateOrderProductsList = viewModel.CalculateOrderProductsList.Where(x => x.Supplier == supplierName);
                return this.View(viewModel);
            }
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult ExecuteOrder(SupplierOrderViewModel supplierOrder)
        {
            var supplierName = ordersService.GetSupplierName();
            var viewModel = new SupplierOrderViewModel();
            viewModel.CalculateOrderProductsList = ordersService.ProductsForOrderList().Where(x => x.Supplier == supplierName && !ordersService.OrdesAny().Any(o => o == x.ProductId));
            if (!ModelState.IsValid)
            {

                viewModel.CalculateOrderProductsList = ordersService.ProductsForOrderList().Where(x => x.Supplier == supplierName);

                return this.View(viewModel);
            }

            ordersService.GenetareOrder(supplierOrder.CalculateOrderProductSingle);

            return this.View(viewModel);
        }
    }
}
