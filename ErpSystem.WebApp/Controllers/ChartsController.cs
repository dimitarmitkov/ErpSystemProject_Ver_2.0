using System;
using ErpSystem.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ErpSystem.WebApp.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ISalesService salesService;
        private readonly IMemoryCache memoryCache;

        public ChartsController(ISalesService salesService, IMemoryCache memoryCache)
        {
            this.salesService = salesService;
            this.memoryCache = memoryCache;
        }

        public IActionResult Chart()
        {
            var viewModel = this.salesService.TotalSalesPerDate();

            return this.View(viewModel);
        }

        public IActionResult JsonChart()
        {
            if (!memoryCache.TryGetValue("Chart", out var viewModel))
            {
                viewModel = this.salesService.TotalSalesPerDate();
                memoryCache.Set("Chart", viewModel, TimeSpan.FromSeconds(20));
            }

            return this.Json(viewModel);
        }
    }
}
