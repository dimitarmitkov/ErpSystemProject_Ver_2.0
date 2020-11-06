using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpSystem.Models
{

    public class Delivery
    {

        private DateTime? deliveryDate = null;

        public int Id { get; set; }

        public virtual Supplier Supplier { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DeliveryDate { get; set; }

        //this is to set DeliveryDate to DateTime now automatically
        //public DateTime DeliveryDate
        //{
        //    get
        //    {
        //        return this.deliveryDate.HasValue
        //           ? this.deliveryDate.Value
        //           : DateTime.Now;
        //    }

        //    set { this.deliveryDate = value; }
        //}


        [Required]
        public decimal TransportCost { get; set; }

        public decimal? CompanyExpenses { get; set; }

        public decimal? OtherExpenses { get; set; }

        [Required]
        public int NumberOfTransportPackageUnits { get; set; }//number of pallets, boxes or other transport package

        public decimal TotalAmountOfProductNumberOrWeight { get; set; }
    }
}
