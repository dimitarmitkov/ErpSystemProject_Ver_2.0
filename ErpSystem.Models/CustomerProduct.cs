namespace ErpSystem.Models
{
    public class CustomerProduct
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public int ProductId { get; set; }

        public int ProductsToSale { get; set; }

        public bool HasProductDiscount { get; set; }

        public int WarehouseProductIdByExpireDate { get; set; }

        public int WarehouseId { get; set; }

        public int CurrentsaleId { get; set; }

        public virtual CurrentSale CurrentSale { get; set; }
    }
}