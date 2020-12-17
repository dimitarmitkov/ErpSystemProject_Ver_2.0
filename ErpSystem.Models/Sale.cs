namespace ErpSystem.Models
{
    using System;

    public class Sale
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int NumberOfSoldProducts { get; set; }

        public DateTime SaleDate { get; set; }

        public decimal SingleProudctSalePrice { get; set; }

        public bool HasProductDiscount { get; set; }

        public bool HasCustomerDiscount { get; set; }
    }
}
