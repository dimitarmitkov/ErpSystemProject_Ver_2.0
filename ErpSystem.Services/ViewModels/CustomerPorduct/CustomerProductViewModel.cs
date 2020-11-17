using System;
namespace ErpSystem.Services.ViewModels.CustomerPorduct
{
    public class CustomerProductViewModel
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int ProductsToSale { get; set; }

        public bool HasProductDiscount { get; set; }

        public int WarehouseId { get; set; }

        public int CurrentsaleId { get; set; }
    }
}
