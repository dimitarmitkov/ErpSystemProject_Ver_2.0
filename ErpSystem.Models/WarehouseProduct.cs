using System;
namespace ErpSystem.Models
{
    public class WarehouseProduct
    {
        public WarehouseProduct()
        {
        }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int ProductsAvailable { get; set; }
    }
}
