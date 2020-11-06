using System;
namespace ErpSystem.Services.ViewModels.Sale
{
    public class SaleViewModel
    {
        public int ProductId { get; set; }

        public string Product { get; set; }

        public string CustomerId { get; set; }

        public string Customer { get; set; }

        public int NumberOfSoldProducts { get; set; }

        public string ProductMesure { get; set; }

        public DateTime SaleDate { get; set; }

        public decimal SingleProudctSalePrice { get; set; }

        public bool HasProductDiscount { get; set; }

        public bool HasCustomerDiscount { get; set; }

        public decimal TotalSalePrice { get; set; }
    }
}
