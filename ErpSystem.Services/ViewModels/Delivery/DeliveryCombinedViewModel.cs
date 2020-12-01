using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ErpSystem.Services.ViewModels.Delivery
{
    public class DeliveryCombinedViewModel
    {
        public IEnumerable<DeliveryListViewModel> List { get; set; }

        public DeliveryListViewModel Single { get; set; }
    }
}
