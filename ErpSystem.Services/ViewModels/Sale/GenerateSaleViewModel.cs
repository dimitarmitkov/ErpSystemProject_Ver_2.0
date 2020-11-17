using System;
namespace ErpSystem.Services.ViewModels.Sale
{
    public class GenerateSaleViewModel
    {
        public string CustomerId { get; set; }

        public string Customer { get; set; }

        public string CompanyType { get; set; }

        public int ProductId { get; set; }

        public string Product { get; set; }

        public string ProductMesure { get; set; }

        public DateTime ProductExpireDate { get; set; }

        public int ProductsAvailable { get; set; }

        public DateTime SaleDate { get; set; }

        public int NumberOfSoldProducts { get; set; }

        public decimal SingleProudctSalePrice { get; set; }

        public bool HasProductDiscount { get; set; }

        public bool HasCustomerDiscount { get; set; }

        public int ProductDiscount { get; set; }

        public int CustomerDiscount { get; set; }

        public decimal TotalSalePrice { get; set; }

        public int WarehouseId { get; set; }
    }
}
