namespace ErpSystem.Services.ViewModels.Delivery
{
    using System.Collections.Generic;

    public class SupplierForDeliveryViewModel
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public IEnumerable<DeliveryListViewModel> Products { get; set; }
    }
}
