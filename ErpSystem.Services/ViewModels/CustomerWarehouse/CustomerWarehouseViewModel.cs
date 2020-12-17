namespace ErpSystem.Services.ViewModels.CustomerWarehouse
{
    using System.Collections.Generic;
    using ErpSystem.Services.ViewModels.CurrentSale;
    using ErpSystem.Services.ViewModels.Warehouse;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CustomerWarehouseViewModel
    {
        public IEnumerable<WarehouseProductViewModel> WarehouseProductCombined { get; set; }

        public WarehouseProductViewModel WarehouseProductSingle { get; set; }

        public CurrentSaleViewModel CustomerCombined { get; set; }

        public IEnumerable<SelectListItem> CustolersListForDD { get; set; }
    }
}
