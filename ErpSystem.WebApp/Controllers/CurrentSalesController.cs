using System;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.CurrentSale;
using ErpSystem.Services.ViewModels.CustomerPorduct;
using ErpSystem.Services.ViewModels.CustomerWarehouse;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.IO;

namespace ErpSystem.WebApp.Controllers
{
    public class CurrentSalesController : Controller
    {
        private readonly ICurrentSalesService currentSales;

        public CurrentSalesController(ICurrentSalesService currentSales)
        {
            this.currentSales = currentSales;
        }

        public IActionResult CurrentSale()
        {
            var viewModel = new CurrentSaleViewModel();

            viewModel.CustomersList = currentSales.SeclectCustomerDropDown();


            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult CurrentSale(CurrentSaleViewModel currentSale)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CurrentSaleViewModel();

                viewModel.CustomersList = currentSales.SeclectCustomerDropDown();

                return this.View(viewModel);
            }

            currentSales.GenerateCurrentSale(currentSale);
            return this.Redirect("/Sales/WarehouseAll");

        }
    }
}
