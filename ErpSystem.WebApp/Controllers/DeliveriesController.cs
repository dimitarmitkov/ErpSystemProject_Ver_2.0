using AutoMapper;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Delivery;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class DeliveriesController : Controller
    {
        private const int ProductsPerPage = 2;
        private const int FirstPage = 1;

        private readonly IDeliveriesService deliveriesService;

        public DeliveriesController(IDeliveriesService deliveriesService, IMapper mapper)
        {
            this.deliveriesService = deliveriesService;
        }

        public IActionResult ConfirmDeliveries(int id = FirstPage)
        {
            var viewModel = new DeliveryCombinedViewModel
            {
                PageNumber = id,
                ProductsCount = this.deliveriesService.GetCount(),
                ItemsPerPage = ProductsPerPage,
                List = this.deliveriesService.GetAllOrdersForDelivery(id, ProductsPerPage),
            };
            //viewModel.List = deliveriesService.GetAllOrdersForDelivery();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult ConfirmDeliveries(DeliveryCombinedViewModel deliveryCombined, int id = FirstPage)
        {
            var viewModel = new DeliveryCombinedViewModel
            {
                PageNumber = id,
                ProductsCount = this.deliveriesService.GetCount(),
                ItemsPerPage = ProductsPerPage,
                List = this.deliveriesService.GetAllOrdersForDelivery(id, ProductsPerPage),
            };
            //var viewModel = new DeliveryCombinedViewModel();
            //viewModel.List = deliveriesService.GetAllOrdersForDelivery(id, 12);
            if (!ModelState.IsValid)
            {
                //viewModel.List = this.deliveriesService.GetAllOrdersForDelivery(id, 12);
                return this.View();
            }


            this.deliveriesService.FinalizeDelivery(deliveryCombined.Single);
            viewModel.List = this.deliveriesService.GetAllOrdersForDelivery(id, ProductsPerPage);

            return this.View(viewModel);
        }
    }
}
