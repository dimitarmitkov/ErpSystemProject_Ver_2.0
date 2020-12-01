using System;
using System.Collections.Generic;

namespace ErpSystem.Services.ViewModels.Sale
{
    public class InvoiceCombinedViewModel
    {
        public IEnumerable<InvoiceViewModel> List { get; set; }

        public IEnumerable<InvoiceViewModel> Single { get; set; }
    }
}
