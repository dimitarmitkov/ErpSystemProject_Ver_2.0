using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.CustomerWarehouse;
using ErpSystem.Services.ViewModels.Order;
using ErpSystem.Services.ViewModels.Sale;
using ErpSystem.Services.ViewModels.Warehouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService salesService;
        //private readonly IWarehouseProductService warehouseProduct;

        public SalesController(ISalesService salesService)
        {
            this.salesService = salesService;
            //this.warehouseProduct = warehouseProduct;
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

        public IActionResult WarehouseAllSelectCustomer()
        {
            CustomerWarehouseViewModel viewModel = new CustomerWarehouseViewModel();

            viewModel.WarehouseProductCombined = salesService.ListOfProductsForSale();
            viewModel.CustolersListForDD = salesService.SeclectCustomerDropDown();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult WarehouseAllSelectCustomer(CustomerWarehouseViewModel customerWarehouse)
        {

            CustomerWarehouseViewModel viewModel = new CustomerWarehouseViewModel();
            if (!ModelState.IsValid)
            {
                viewModel.WarehouseProductCombined = salesService.ListOfProductsForSale();
                viewModel.CustolersListForDD = salesService.SeclectCustomerDropDown();
                return this.View(viewModel);
            }

            var customerEik = customerWarehouse.CustomerCombined.CustomerName;
            var hasDiscount = customerWarehouse.CustomerCombined.HasCustomerDiscount;

            salesService.GenerateCurrentSale(customerEik, hasDiscount);

            return this.Redirect("/Sales/WarehouseAllGenerateSale");
        }


        public IActionResult WarehouseAllGenerateSale()
        {
            var deliveryNeedCheck = salesService.AreAnyProductsForOrder().ToList();
            if (deliveryNeedCheck.Any())
            {
                return this.RedirectToAction("OrderNeeded");
            }

            CustomerWarehouseViewModel viewModel = new CustomerWarehouseViewModel();

            viewModel.WarehouseProductCombined = salesService.ListOfProductsForSaleWithCustomer();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult WarehouseAllGenerateSale(CustomerWarehouseViewModel customerWarehouse)
        {

            if (!ModelState.IsValid)
            {
                CustomerWarehouseViewModel viewModel = new CustomerWarehouseViewModel();
                viewModel.WarehouseProductCombined = salesService.ListOfProductsForSaleWithCustomer();
                return this.View(viewModel);
            }

            var productId = customerWarehouse.WarehouseProductSingle.ProductId;
            var customerId = customerWarehouse.WarehouseProductSingle.CustomerId;
            var numberOfSoldProducts = customerWarehouse.WarehouseProductSingle.ProductSold;
            var hasProductDiscount = customerWarehouse.WarehouseProductSingle.HasProductDiscount;
            var hasCustomerDiscount = customerWarehouse.WarehouseProductSingle.HasCustomerDiscount;
            var warehouseId = customerWarehouse.WarehouseProductSingle.WarehouseId;
            var warehouseProductId = customerWarehouse.WarehouseProductSingle.WarehouseProductId;

            salesService.CreateSale(productId, customerId, numberOfSoldProducts, hasProductDiscount, hasCustomerDiscount, warehouseId, warehouseProductId);

            return this.Redirect("/Sales/WarehouseAllGenerateSale");
        }

        public IActionResult OrderNeeded()
        {
            var viewModel = salesService.AreAnyProductsForOrder();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult OrderNeeded(DeliveryNeededProduct deliveryNeededProduct)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            salesService.ConfirmNeedOfOrder(deliveryNeededProduct);
            return this.Redirect("/Sales/WarehouseAllGenerateSale");
        }

    }
}
