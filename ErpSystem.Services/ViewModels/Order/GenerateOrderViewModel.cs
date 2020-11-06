using System;
namespace ErpSystem.Services.ViewModels.Order
{
    public class GenerateOrderViewModel
    {
        public int ProductId { get; set; }

        public virtual string Supplier { get; set; }

        public int CalculatedOrderProductNumber { get; set; }

        public int ReserveNumberOfOrderedProduct { get; set; }

        public DateTime OrderDate { get; set; }

        public int NumberOfTransportPackageUnits { get; set; }

        public decimal TotalAmountOfProductNumberOrWeight { get; set; }
    }
}
