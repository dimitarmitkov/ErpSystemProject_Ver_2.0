namespace ErpSystem.WebApp.Controllers
{
    using ErpSystem.Services.Services;
    using Microsoft.AspNetCore.Mvc;

    public class WarehousesController : Controller
    {
        private readonly IWarehouseSpace warehouseSpace;

        public WarehousesController(IWarehouseSpace warehouseSpace)
        {
            this.warehouseSpace = warehouseSpace;
        }

        public IActionResult JsonChartBoxes()
        {
            var viewModel = this.warehouseSpace.GetSpaceBoxes();
            return this.Json(viewModel);
        }

        public IActionResult JsonChartPallets()
        {
            var viewModel = this.warehouseSpace.GetSpacePallets();
            return this.Json(viewModel);
        }
    }
}
