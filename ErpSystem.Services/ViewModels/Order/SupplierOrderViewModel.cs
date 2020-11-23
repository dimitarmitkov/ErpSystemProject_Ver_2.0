using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.ViewModels.Order
{
    public class SupplierOrderViewModel
    {
        public IEnumerable<CalculateNeedOfOrderViewModel> CalculateOrderProductsList { get; set; }

        public CalculateNeedOfOrderViewModel CalculateOrderProductSingle { get; set; }

        public IEnumerable<SelectListItem> SuppliersListDropDown { get; set; }

        public CalculateNeedOfOrderViewModel Suppliers { get; set; }

    }
}
