using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetTopologySuite.Simplify;

namespace ErpSystem.Services.ViewModels.Order
{
    public class CalculateNeedOfOrderViewModel
    {
        public int ProductId { get; set; }

        public string Product { get; set; }

        public string ProductMeasurementType { get; set; }

        public string Supplier { get; set; }

        public int OrderDays { get; set; }

        public int DeliveryDays { get; set; }

        public int TotalDeliveryTime { get; set; }

        public int ProductsAvailable { get; set; }

        public int SalesBasedOnDeliveryPeriod { get; set; }

        public int NumberOfProductsInTrasportUnit { get; set; }

        public int NumberOfTransportUnits { get; set; }

        public decimal ProductExwPrice { get; set; }

        public decimal TotalProductPrice { get; set; }

        public double TotalWeightOfTransportUnit { get; set; }

        public bool ConfimBeenNoticed { get; set; }

        [Range(0, 1000, ErrorMessage = "Please input number between 0 and 1000")]
        public int NumberOfTransportUnitsClaculatedForOrder { get; set; }

        [Range(0, 1000, ErrorMessage = "Please input number between 0 and 1000")]
        public int OrderedTarnsportUnits { get; set; }

        public string Package { get; set; }

        public IEnumerable<SelectListItem> SuppliersDropDown { get; set; }
    }
}
