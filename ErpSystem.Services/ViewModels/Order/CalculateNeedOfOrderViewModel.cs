namespace ErpSystem.Services.ViewModels.Order
{
    public class CalculateNeedOfOrderViewModel
    {
        public int ProductId { get; set; }

        public string Product { get; set; }

        public string Supplier { get; set; }

        public int OrderDays { get; set; }

        public int DeliveryDays { get; set; }

        public int TotalDeliveryTime { get; set; }

        public int ProductsAvailable { get; set; }

        public int SalesBasedOnDeliveryPeriod { get; set; }

        public bool ConfimBeenNoticed { get; set; }
    }
}
