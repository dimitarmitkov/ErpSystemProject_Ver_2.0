namespace ErpSystem.Services.ViewModels.Supplier
{
    using System.ComponentModel.DataAnnotations;

    public class AddSupplierViewModel
    {
        [Required(ErrorMessage = "Please insert supplier name")]
        [MinLength(3, ErrorMessage = " Supplier name ought to be at least 3 characters")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "Please insert supplier country")]
        [MinLength(3, ErrorMessage = " Supplier country ought to be at least 3 characters")]
        public string SupplierCountry { get; set; }

        [Required(ErrorMessage = "Please insert supplier postal code")]
        public string SupplierPostalCode { get; set; }

        [Required(ErrorMessage = "Please insert supplier address")]
        public string SupplierAddress { get; set; }

        [Required(ErrorMessage = "Please insert supplier phone")]
        [Phone]
        public string PhoneNumber { get; set; } //TODO must contain +359(or other prefix code) ...;

        [Required(ErrorMessage = "Please insert supplier email")]
        [EmailAddress]
        public string Email { get; set; } //TODO email check

        public bool CustomsAuthorisationNeeded { get; set; }

        public string SupplierAdditionalInformation { get; set; }
    }
}
