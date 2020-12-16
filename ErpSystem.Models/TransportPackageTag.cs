namespace ErpSystem.Models
{
    using System.Collections.Generic;

    public class TransportPackageTag
    {
        public TransportPackageTag()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string TypeOfPackage { get; set; } // box, pallet, container...

        public ICollection<Product> Products { get; set; }
    }
}
