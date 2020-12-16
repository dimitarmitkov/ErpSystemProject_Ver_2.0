namespace ErpSystem.Models
{
    public class WarehouseBoxSpace
    {
        public int Id { get; set; }

        public int WarehouseNumber { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public int BoxSpace { get; set; }

        public int ShelfDepth { get; set; }
    }
}
