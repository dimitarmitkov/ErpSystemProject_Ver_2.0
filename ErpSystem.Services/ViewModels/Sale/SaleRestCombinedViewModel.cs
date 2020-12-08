using System;
using System.Collections.Generic;

namespace ErpSystem.Services.ViewModels.Sale
{
    public class SaleRestCombinedViewModel
    {
        public IEnumerable<SalesPerCustomerOrProductViewModel> List { get; set; }

        public SalesPerCustomerOrProductViewModel Single { get; set; }
    }
}
