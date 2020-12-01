using System;
using AutoMapper;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Delivery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ErpSystem.WebApp.Controllers
{
    public class DeliveriesController : Controller
    {
        private readonly IDeliveriesService deliveriesService;

        public DeliveriesController(IDeliveriesService deliveriesService, IMapper mapper)
        {
            this.deliveriesService = deliveriesService;
        }

        public IActionResult ConfirmDeliveries()
        {
            var viewModel = new DeliveryCombinedViewModel();
            viewModel.List = deliveriesService.GetAllOrdersForDelivery();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult ConfirmDeliveries(DeliveryCombinedViewModel deliveryCombined)
        {
            var viewModel = new DeliveryCombinedViewModel();
            viewModel.List = deliveriesService.GetAllOrdersForDelivery();
            if (!ModelState.IsValid)
            {
                viewModel.List = deliveriesService.GetAllOrdersForDelivery();
                return this.View();
            }


            deliveriesService.FinalizeDelivery(deliveryCombined.Single);
            viewModel.List = deliveriesService.GetAllOrdersForDelivery();

            return this.View(viewModel);
        }
    }
}
