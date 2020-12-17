namespace ErpSystem.WebApp.Controllers
{
    using System.Linq;
    using ErpSystem.Models;
    using ErpSystem.Services.Services;
    using ErpSystem.Services.ViewModels.CustomerWarehouse;
    using ErpSystem.Services.ViewModels.Sale;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SalesController : Controller
    {
        private readonly ISalesService salesService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public SalesController(ISalesService salesService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.salesService = salesService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult All()
        {
            var viewModel = this.salesService.ListOfAllSales();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult All(string customer, string product)
        {
            var viewModel = this.salesService.ListOfAllSales();

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

        public IActionResult WarehouseAllSelectCustomer()
        {
            CustomerWarehouseViewModel viewModel = new CustomerWarehouseViewModel();

            viewModel.WarehouseProductCombined = this.salesService.ListOfProductsForSale();
            viewModel.CustolersListForDD = this.salesService.SeclectCustomerDropDown();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult WarehouseAllSelectCustomer(CustomerWarehouseViewModel customerWarehouse)
        {
            CustomerWarehouseViewModel viewModel = new CustomerWarehouseViewModel();
            if (!this.ModelState.IsValid)
            {
                viewModel.WarehouseProductCombined = this.salesService.ListOfProductsForSale();
                viewModel.CustolersListForDD = this.salesService.SeclectCustomerDropDown();
                return this.View(viewModel);
            }

            var customerEik = customerWarehouse.CustomerCombined.CustomerName;
            var hasDiscount = customerWarehouse.CustomerCombined.HasCustomerDiscount;
            var isSignedIn = this.signInManager.IsSignedIn(this.User);

            if (isSignedIn)
            {
                var userId = this.userManager.GetUserId(this.User);
                this.salesService.GenerateCurrentSale(customerEik, hasDiscount, userId);
                return this.Redirect("/Sales/WarehouseAllGenerateSale");
            }

            return this.View();
        }

        public IActionResult WarehouseAllGenerateSale()
        {
            var deliveryNeedCheck = this.salesService.AreAnyProductsForOrder().ToList();
            if (deliveryNeedCheck.Any())
            {
                return this.RedirectToAction("OrderNeeded");
            }

            CustomerWarehouseViewModel viewModel = new CustomerWarehouseViewModel();
            viewModel.WarehouseProductCombined = this.salesService.ListOfProductsForSaleWithCustomer();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult WarehouseAllGenerateSale(CustomerWarehouseViewModel customerWarehouse)
        {
            if (!this.ModelState.IsValid)
            {
                CustomerWarehouseViewModel viewModel = new CustomerWarehouseViewModel();
                viewModel.WarehouseProductCombined = this.salesService.ListOfProductsForSaleWithCustomer();
                return this.View(viewModel);
            }

            var customerWarehouseProducts = customerWarehouse.WarehouseProductSingle;

            var productId = customerWarehouseProducts.ProductId;
            var customerId = customerWarehouseProducts.CustomerId;
            var numberOfSoldProducts = customerWarehouseProducts.ProductSold;
            var hasProductDiscount = customerWarehouseProducts.HasProductDiscount;
            var hasCustomerDiscount = customerWarehouseProducts.HasCustomerDiscount;
            var warehouseId = customerWarehouseProducts.WarehouseId;
            var warehouseProductId = customerWarehouseProducts.WarehouseProductId;

            this.salesService.CreateSale(productId, customerId, numberOfSoldProducts, hasProductDiscount, hasCustomerDiscount, warehouseId, warehouseProductId);

            return this.Redirect("/Sales/WarehouseAllGenerateSale");
        }

        public IActionResult OrderNeeded()
        {
            var viewModel = this.salesService.AreAnyProductsForOrder();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult OrderNeeded(DeliveryNeededProduct deliveryNeededProduct)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.salesService.ConfirmNeedOfOrder(deliveryNeededProduct);
            return this.Redirect("/Sales/WarehouseAllGenerateSale");
        }

        public IActionResult Invoice()
        {
            var viewModel = new InvoiceCombinedViewModel();
            viewModel.List = this.salesService.Invoice();
            viewModel.Single = this.salesService.Invoice();

            if (viewModel.List.ToList().Count != 0 && viewModel.Single.ToList().Count != 0)
            {
                return this.View(viewModel);
            }

            return this.Redirect("/Errors/Error500");
        }
    }
}