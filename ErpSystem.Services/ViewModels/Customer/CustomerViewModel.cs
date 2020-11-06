using System;
namespace ErpSystem.Services.Models
{
    public class CustomerViewModel
    {
        public string CompanyName { get; set; }

        public string CompanyType { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; } //TODO int must contain 4 numbers

        public string Address { get; set; }

        public string PhoneNumber { get; set; } //TODO must contain +359(or other prefix code) ...;

        public string Email { get; set; } //TODO email check

        public int? CustomerDiscount { get; set; }

        public bool HasDelivery { get; set; }

        public bool IsActive { get; set; }

        public string AdditionalInfo { get; set; }


    }
}
