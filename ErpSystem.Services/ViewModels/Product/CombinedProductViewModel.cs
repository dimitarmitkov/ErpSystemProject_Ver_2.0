using System;
using System.Collections.Generic;

namespace ErpSystem.Services.ViewModels.Product
{
    public class CombinedProductViewModel
    {
        public IEnumerable<ProductViewModel> ProductList { get; set; }

        public ProductViewModel ProductSingle { get; set; }
    }
}
