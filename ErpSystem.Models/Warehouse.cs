namespace ErpSystem.Models
{
    using System.Collections.Generic;

    public class Warehouse
    {
        public Warehouse()
        {
            this.WarehouseProducts = new HashSet<WarehouseProduct>();
        }

        public int Id { get; set; }

        public string WarehouseName { get; set; }

        public int CurrentPalletsSpaceFree { get; set; }

        public int CurrentBoxesFrontSpaceFree { get; set; }

        public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; }
    }
}
