namespace ErpSystem.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ErpSystem.Services.ViewModels.Delivery;

    public interface IDeliveriesService
    {
        IEnumerable<DeliveryListViewModel> GetAllOrdersForDelivery(int page, int itemsPerPage = 4);

        void FinalizeDelivery(DeliveryListViewModel deliveryList);

        int GetCount();
    }
}
