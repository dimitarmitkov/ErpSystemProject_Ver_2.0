namespace ErpSystem.Services.ViewModels.CurrentSale
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CurrentSaleViewModel
    {
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEikNumber { get; set; }

        public bool HasCustomerDiscount { get; set; }

        public IEnumerable<SelectListItem> CustomersList { get; set; }
    }
}
