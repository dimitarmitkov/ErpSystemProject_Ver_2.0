namespace ErpSystem.Services.ViewModels.Customer
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GenerateSaleCustomerViewModel
    {
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEikNumber { get; set; }

        public bool HasCustomerDiscount { get; set; }

        public IEnumerable<SelectListItem> CustomersNames { get; set; }
    }
}
