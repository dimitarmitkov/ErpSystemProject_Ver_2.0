namespace ErpSystem.WebApp.Controllers
{
    using ErpSystem.Services.Services;
    using ErpSystem.Services.ViewModels.Customer;
    using Microsoft.AspNetCore.Mvc;

    public class CustomersController : Controller
    {
        private readonly ICustomersService customersService;

        public CustomersController(ICustomersService customersService)
        {
            this.customersService = customersService;
        }

        public IActionResult CreateCustomer()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(CustomerViewModel customerView)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.customersService.CreateCustomer(customerView);
            return this.Redirect("/Home/Index");
        }
    }
}
