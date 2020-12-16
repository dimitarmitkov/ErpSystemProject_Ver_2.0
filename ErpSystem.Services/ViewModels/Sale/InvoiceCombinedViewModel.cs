namespace ErpSystem.Services.ViewModels.Sale
{
    using System.Collections.Generic;

    public class InvoiceCombinedViewModel
    {
        public IEnumerable<InvoiceViewModel> List { get; set; }

        public IEnumerable<InvoiceViewModel> Single { get; set; }
    }
}
