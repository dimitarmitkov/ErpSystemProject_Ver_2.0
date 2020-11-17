using System;
namespace ErpSystem.Services.ViewModels.Warehouse
{
    public class WarehouseProductViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Product { get; set; }

        public string ProductMeasurement { get; set; }

        public int WarehouseId { get; set; }

        public int ProductsAvailable { get; set; }

        public DateTime? ProductExpireDate { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public bool HasCustomerDiscount { get; set; }

        public bool HasProductDiscount { get; set; }

        public int ProductSold { get; set; }

        public int WarehouseProductId { get; set; }
    }
}
