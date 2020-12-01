using System;
using ErpSystem.Services.ViewModels.Order;

namespace ErpSystem.Services.ViewModels.Delivery
{
    public class DeliveryListViewModel : CalculateNeedOfOrderViewModel
    {
        public decimal TotalOrderPrice { get; set; }

        public double TotalOrderWeight { get; set; }

        public int ChangedNumberOfTransportUnits { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpireDate { get; set; }
    }
}
