namespace ErpSystem.WebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorsController : Controller
    {
        public IActionResult Error500()
        {
            return this.View();
        }

        public IActionResult Error404()
        {
            return this.View();
        }

        public IActionResult Error401()
        {
            return this.View();
        }
    }
}