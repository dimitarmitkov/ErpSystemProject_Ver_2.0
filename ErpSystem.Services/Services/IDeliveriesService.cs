using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpSystem.Services.ViewModels.Delivery;

namespace ErpSystem.Services.Services
{
    public interface IDeliveriesService
    {
        IEnumerable<DeliveryListViewModel> GetAllOrdersForDelivery(int page, int itemsPerPage = 4);

        void FinalizeDelivery(DeliveryListViewModel deliveryList);

        int GetCount();
    }
}
