using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.ViewModels.CurrentSale
{
    public class CurrentSaleViewModel
    {
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEikNumber { get; set; }

        public bool HasCustomerDiscount { get; set; }

        public IEnumerable<SelectListItem> CustomersList { get; set; }
    }
}
