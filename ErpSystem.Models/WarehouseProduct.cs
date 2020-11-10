using System;
namespace ErpSystem.Models
{
    public class WarehouseProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int ProductsAvailable { get; set; }
    }
}
