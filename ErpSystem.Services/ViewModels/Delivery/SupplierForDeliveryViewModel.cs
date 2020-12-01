using System.Collections.Generic;

namespace ErpSystem.Services.ViewModels.Delivery
{
    public class SupplierForDeliveryViewModel
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public IEnumerable<DeliveryListViewModel> Products { get; set; }
    }
}
