namespace ErpSystem.Models
{
    using System.Collections.Generic;

    public class ProductMeasurmentTag
    {
        public ProductMeasurmentTag()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string Maesurment { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
