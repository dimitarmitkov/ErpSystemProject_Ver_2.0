using System;
using ErpSystem.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly IWarehouseSpace warehouseSpace;

        public WarehousesController(IWarehouseSpace warehouseSpace)
        {
            this.warehouseSpace = warehouseSpace;
        }

        public IActionResult JsonChartBoxes()
        {
            var viewModel = warehouseSpace.GetSpaceBoxes();
            return this.Json(viewModel);
        }

        public IActionResult JsonChartPallets()
        {
            var viewModel = warehouseSpace.GetSpacePallets();
            return this.Json(viewModel);
        }

    }
}
