using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Supplier;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISuppliersService suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            this.suppliersService = suppliersService;
        }

        public IActionResult Supplier()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Supplier(AddSupplierViewModel addSupplier)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            suppliersService.AddSupplier(addSupplier);
            return this.Redirect("/Home/Index");
        }
    }
}
