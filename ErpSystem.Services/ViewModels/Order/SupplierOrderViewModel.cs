namespace ErpSystem.Services.ViewModels.Order
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SupplierOrderViewModel
    {
        public IEnumerable<CalculateNeedOfOrderViewModel> CalculateOrderProductsList { get; set; }

        public CalculateNeedOfOrderViewModel CalculateOrderProductSingle { get; set; }

        public IEnumerable<SelectListItem> SuppliersListDropDown { get; set; }

        public CalculateNeedOfOrderViewModel Suppliers { get; set; }
    }
}
