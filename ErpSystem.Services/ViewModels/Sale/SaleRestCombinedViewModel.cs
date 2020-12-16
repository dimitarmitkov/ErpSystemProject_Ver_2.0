namespace ErpSystem.Services.ViewModels.Sale
{
    using System.Collections.Generic;

    public class SaleRestCombinedViewModel
    {
        public IEnumerable<SalesPerCustomerOrProductViewModel> List { get; set; }

        public SalesPerCustomerOrProductViewModel Single { get; set; }
    }
}
