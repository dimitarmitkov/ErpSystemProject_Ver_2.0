namespace ErpSystem.Models
{
    using System.Collections.Generic;

    public class CurrentSale
    {
        public CurrentSale()
        {
            this.CusomerProducts = new HashSet<CustomerProduct>();
        }

        public int Id { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEikNumber { get; set; }

        public bool HasCustomerDiscount { get; set; }

        public virtual ICollection<CustomerProduct> CusomerProducts { get; set; }

        public string UserId { get; set; }
    }
}
