using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
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