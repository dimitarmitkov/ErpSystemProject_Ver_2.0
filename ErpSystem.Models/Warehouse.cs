using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string WarehouseName { get; set; }

        public Warehouse()
        {
            this.WarehouseProducts = new HashSet<WarehouseProduct>();
        }

        public int CurrentPalletsSpaceFree { get; set; }

        public int CurrentBoxesFrontSpaceFree { get; set; }

        public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; }
    }
}
