using System;
using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Services.ViewModels.Customer
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "Please insert customer name")]
        [MinLength(3, ErrorMessage = " Supplier name ought to be at least 3 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please insert customer type of registration")]
        [MinLength(2, ErrorMessage = " Customer type of registration ought to be at least 2 characters")]
        public string CompanyType { get; set; }

        [Required(ErrorMessage = "Please insert city name")]
        [MinLength(3, ErrorMessage = "City name ought to be at least 3 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please insert postal code")]
        [RegularExpression(@"[\d]+[ |-][\d]+|[\d]+", ErrorMessage = "Please enter correct postal code")]
        public int PostalCode { get; set; } //TODO int must contain 4 numbers

        [Required(ErrorMessage = "Please insert address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please insert phone number")]
        [Phone]
        public string PhoneNumber { get; set; } //TODO must contain +359(or other prefix code) ...;

        [Required(ErrorMessage = "Please insert email")]
        [EmailAddress]
        public string Email { get; set; } //TODO email check

        [Range(0, 100)]
        public int? CustomerDiscount { get; set; }

        [Required(ErrorMessage = "Please check if delivery is needed")]
        public bool HasDelivery { get; set; }

        public bool IsActive { get; set; }

        public string CustomerAdditionalInfo { get; set; }

        [Required(ErrorMessage = "Please insert correct EIK number")]
        [RegularExpression(@"^BG[\d]{9}$", ErrorMessage = "Please enter correct EIK")]
        public string CompanyEik { get; set; }
    }
}
