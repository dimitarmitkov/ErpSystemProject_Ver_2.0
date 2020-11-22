using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpSystem.Services.ViewModels.Order;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.Services
{
    public interface IOrdersService
    {
        Task GenetareOrder();

        IEnumerable<SelectListItem> SuppliersDropDown();

        IEnumerable<CalculateNeedOfOrderViewModel> ProductsForOrderList();
    }
}
