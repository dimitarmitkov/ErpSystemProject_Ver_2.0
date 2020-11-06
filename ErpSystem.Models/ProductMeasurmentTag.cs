using System;
using System.Collections.Generic;

namespace ErpSystem.Models
{
    public class ProductMeasurmentTag
    {
        public ProductMeasurmentTag()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string Maesurment { get; set; }//pieces, kg, liter...

        public virtual ICollection<Product> Products { get; set; }
    }
}
