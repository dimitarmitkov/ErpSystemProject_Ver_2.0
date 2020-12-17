namespace ErpSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class Supplier
    {
        public Supplier()
        {
            this.Products = new HashSet<Product>();

            this.Deliveries = new HashSet<Delivery>();

            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        public string SupplierName { get; set; }

        public string SupplierCountry { get; set; }

        [RegularExpression(@"[\d]+[ |-][\d]+|[\d]+")]
        public string SupplierPostalCode { get; set; }

        public string SupplierAddress { get; set; }

        [Required]
        [RegularExpression(@"\+[\d]{2,3}[ ][\d]+[ ][\d]{1,13}|\+[\d]{2,3}[ ][\d]+")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public bool CustomsAuthorisationNeeded { get; set; }

        public string SupplierAdditionalInformation { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}