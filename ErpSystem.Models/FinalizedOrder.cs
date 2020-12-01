using System;
namespace ErpSystem.Models
{
    public class FinalizedOrder
    {
        public int Id { get; set; }

        public int SupplierId { get; set; }

        public string Supplier { get; set; }

        public DateTime OrderDate { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int NumberOfTransportPackageUnitsOrdered { get; set; }

        public decimal TotalAmountOfOrder { get; set; }

        public double TotalOrderWeight { get; set; }
    }
}
