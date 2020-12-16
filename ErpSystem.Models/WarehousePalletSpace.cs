namespace ErpSystem.Models
{
    public class WarehousePalletSpace
    {
        public int Id { get; set; }

        public int WarehouseNumber { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public int PalletSpace { get; set; }
    }
}
