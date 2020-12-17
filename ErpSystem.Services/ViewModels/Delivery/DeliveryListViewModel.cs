namespace ErpSystem.Services.ViewModels.Delivery
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ErpSystem.Services.ViewModels.Order;

    public class DeliveryListViewModel : CalculateNeedOfOrderViewModel
    {
        public decimal TotalOrderPrice { get; set; }

        public double TotalOrderWeight { get; set; }

        [Required(ErrorMessage = "Please enter number of ordered units to confirm delivery.")]
        public int ChangedNumberOfTransportUnits { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpireDate { get; set; }
    }
}