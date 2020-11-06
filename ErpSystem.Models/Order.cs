using System;
namespace ErpSystem.Models
{
    public class Order
    {
        public int Id { get; set; }

        public virtual Supplier Supplier { get; set; }

        public int CalculatedOrderProductNumber { get; set; }

        public int ReserveNumberOfOrderedProduct { get; set; }

        public DateTime OrderDate { get; set; }

        public int NumberOfTransportPackageUnits { get; set; }//number of pallets, boxes or other transport package

        public decimal TotalAmountOfProductNumberOrWeight { get; set; }
    }
}
