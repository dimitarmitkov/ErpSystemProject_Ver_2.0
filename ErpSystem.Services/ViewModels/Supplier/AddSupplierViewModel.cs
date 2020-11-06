using System;
namespace ErpSystem.Services.ViewModels.Supplier
{
    public class AddSupplierViewModel
    {
        public string SupplierName { get; set; }

        public string SupplierCountry { get; set; }

        public string SupplierPostalCode { get; set; }

        public string SupplierAddress { get; set; }

        public string PhoneNumber { get; set; } //TODO must contain +359(or other prefix code) ...;

        public string Email { get; set; } //TODO email check

        public bool CustomsAuthorisationNeeded { get; set; }

        public string SupplierAdditionalInformation { get; set; }
    }
}
