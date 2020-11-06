using System;
namespace ErpSystem.Models
{
    public class ProductSupplier
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
