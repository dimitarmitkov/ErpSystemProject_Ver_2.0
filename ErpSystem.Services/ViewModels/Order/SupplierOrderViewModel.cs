using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.ViewModels.Order
{
    public class SupplierOrderViewModel
    {
        public IEnumerable<CalculateNeedOfOrderViewModel> CalculateOrderProductsList { get; set; }

        public IEnumerable<SelectListItem> SuppliersListDropDown { get; set; }

    }
}
