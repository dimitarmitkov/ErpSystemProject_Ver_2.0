namespace ErpSystem.Services.ViewModels.Warehouse
{
    using System;

    public class AddProductWaerhouseViewModel
    {
        public int AddQuantity { get; set; }

        public int SpaceTaken { get; set; }

        public int ProductId { get; set; }

        public int WarehouseId { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? ExpireDate { get; set; }
    }
}
