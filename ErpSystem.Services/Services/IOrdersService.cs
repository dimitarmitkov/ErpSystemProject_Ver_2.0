namespace ErpSystem.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ErpSystem.Services.ViewModels.Order;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IOrdersService
    {
        void GenetareOrder(CalculateNeedOfOrderViewModel calculateNeedOfOrder);

        IEnumerable<SelectListItem> SuppliersDropDown();

        IEnumerable<CalculateNeedOfOrderViewModel> ProductsForOrderList();

        void SelectSupplier(string supplierName);

        string GetSupplierName();

        Task FinalizeOrder();

        IEnumerable<int> OrdesAny();
    }
}