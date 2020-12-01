using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpSystem.Services.ViewModels.Delivery;

namespace ErpSystem.Services.Services
{
    public interface IDeliveriesService
    {
        IEnumerable<DeliveryListViewModel> GetAllOrdersForDelivery();

        Task FinalizeDelivery(DeliveryListViewModel deliveryList);
    }
}
