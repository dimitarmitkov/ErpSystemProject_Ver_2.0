using System.Collections.Generic;
using ErpSystem.Services.ViewModels.CurrentSale;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.Services
{
    public interface ICurrentSalesService
    {
        void GenerateCurrentSale(CurrentSaleViewModel currentSale);

        IEnumerable<SelectListItem> SeclectCustomerDropDown();

    }
}
