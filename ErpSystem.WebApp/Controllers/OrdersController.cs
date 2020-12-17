namespace ErpSystem.WebApp.Controllers
{
    using System.Linq;
    using ErpSystem.Services.Services;
    using ErpSystem.Services.ViewModels.Order;
    using Microsoft.AspNetCore.Mvc;

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
            viewModel.CalculateOrderProductsList = this.ordersService.ProductsForOrderList();
            viewModel.SuppliersListDropDown = this.ordersService.SuppliersDropDown();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult GenerateOrder(SupplierOrderViewModel supplierOrder)
        {
            var viewModel = new SupplierOrderViewModel();
            if (!this.ModelState.IsValid)
            {
                viewModel.CalculateOrderProductsList = this.ordersService.ProductsForOrderList();
                viewModel.SuppliersListDropDown = this.ordersService.SuppliersDropDown();

                return this.View(viewModel);
            }

            var supplierName = supplierOrder.Suppliers.Supplier;

            this.ordersService.SelectSupplier(supplierName);

            viewModel.CalculateOrderProductsList = this.ordersService.ProductsForOrderList();

            if (!string.IsNullOrEmpty(supplierName))
            {
                viewModel.CalculateOrderProductsList = viewModel.CalculateOrderProductsList.Where(x => x.Supplier == supplierName);
                return this.Redirect("/Orders/ExecuteOrder");
            }

            return this.View(viewModel);
        }

        public IActionResult ExecuteOrder()
        {
            var supplierName = this.ordersService.GetSupplierName();

            var viewModel = new SupplierOrderViewModel();
            if (!this.ModelState.IsValid)
            {
                viewModel.CalculateOrderProductsList = this.ordersService.ProductsForOrderList();

                return this.View(viewModel);
            }

            viewModel.CalculateOrderProductsList = this.ordersService.ProductsForOrderList();

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
            var supplierName = this.ordersService.GetSupplierName();
            var viewModel = new SupplierOrderViewModel();
            viewModel.CalculateOrderProductsList = this.ordersService.ProductsForOrderList().Where(x => x.Supplier == supplierName && !this.ordersService.OrdesAny().Any(o => o == x.ProductId));
            if (!this.ModelState.IsValid)
            {
                viewModel.CalculateOrderProductsList = this.ordersService.ProductsForOrderList().Where(x => x.Supplier == supplierName);

                return this.View(viewModel);
            }

            this.ordersService.GenetareOrder(supplierOrder.CalculateOrderProductSingle);

            return this.View(viewModel);
        }
    }
}
