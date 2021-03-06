﻿namespace ErpSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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

        public string CompanyEik { get; set; }

        public virtual CompanyTypeTag CompanyTypeOfRegistration { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [RegularExpression(@"[\d]+[ |-][\d]+|[\d]+")]
        public int PostalCode { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"\+[\d]{2,3}[ ][\d]+[ ][\d]{1,13}|\+[\d]{2,3}[ ][\d]+")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int? CustomerType { get; set; }

        public int? CustomerRating { get; set; }

        public int? CustomerDiscount { get; set; }

        public bool HasDelivery { get; set; }

        public bool IsActive { get; set; }

        public string CustomerAdditionalInfo { get; set; }

        public virtual ICollection<Sale> Purchase { get; set; }
    }
}