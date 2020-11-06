using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpSystem.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Purchase = new HashSet<Sale>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public int CompanyTypeId { get; set; }

        public virtual CompanyTypeTag CompanyTypeOfRegistration { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [RegularExpression(@"[\d]+[ |-][\d]+|[\d]+")]
        public int PostalCode { get; set; } //TODO int must contain 4 numbers

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"\+[\d]{2,3}[ ][\d]+[ ][\d]{1,13}|\+[\d]{2,3}[ ][\d]+")]
        public string PhoneNumber { get; set; } //TODO must contain +359(or other prefix code) ...;

        [EmailAddress]
        public string Email { get; set; } //TODO email check

        public int? CustomerType { get; set; }

        public int? CustomerRating { get; set; }

        public int? CustomerDiscount { get; set; }

        public bool HasDelivery { get; set; }

        public bool IsActive { get; set; }

        public string CustomerAdditionalInfo { get; set; }

        public virtual ICollection<Sale> Purchase { get; set; }

    }
}
