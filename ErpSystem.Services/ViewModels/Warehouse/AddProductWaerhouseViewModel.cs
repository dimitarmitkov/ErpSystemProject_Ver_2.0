using System;
namespace ErpSystem.Services.ViewModels.Warehouse
{
    public class AddProductWaerhouseViewModel
    {
        public int AddQuantity { get; set; }

        public int SpaceTaken { get; set; }

        public int ProductId { get; set; }

        public int WarehouseId { get; set; }
    }
}
