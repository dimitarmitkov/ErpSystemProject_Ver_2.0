using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpSystem.Models
{
    public class SaleAccumulator
    {
        [ForeignKey("Sales")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int SoldProductsAccumulator { get; set; }

        public int SoldBoxCounter { get; set; }

        public int SoldPalletCounter { get; set; }
    }
}
