﻿namespace ErpSystem.WebApp.Controllers
{
    using ErpSystem.Services.Services;
    using ErpSystem.Services.ViewModels.Delivery;
    using Microsoft.AspNetCore.Mvc;

    public class DeliveriesController : Controller
    {
        private const int ProductsPerPage = 2;
        private const int FirstPage = 1;
        private readonly IDeliveriesService deliveriesService;

        public DeliveriesController(IDeliveriesService deliveriesService)
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

            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            this.deliveriesService.FinalizeDelivery(deliveryCombined.Single);
            viewModel.List = this.deliveriesService.GetAllOrdersForDelivery(id, ProductsPerPage);

            return this.View(viewModel);
        }
    }
}