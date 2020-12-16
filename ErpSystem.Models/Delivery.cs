namespace ErpSystem.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;

    public class Delivery
    {
        public int Id { get; set; }

        public virtual Supplier Supplier { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public decimal TransportCost { get; set; }

        public decimal? CompanyExpenses { get; set; }

        public decimal? OtherExpenses { get; set; }

        [Required]
        public int NumberOfTransportPackageUnits { get; set; }

        public decimal TotalAmountOfProductNumberOrWeight { get; set; }
    }
}