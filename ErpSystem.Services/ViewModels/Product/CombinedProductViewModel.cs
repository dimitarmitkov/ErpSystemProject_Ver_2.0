namespace ErpSystem.Services.ViewModels.Product
{
    using System.Collections.Generic;

    public class CombinedProductViewModel
    {
        public IEnumerable<ProductViewModel> ProductList { get; set; }

        public ProductViewModel ProductSingle { get; set; }
    }
}