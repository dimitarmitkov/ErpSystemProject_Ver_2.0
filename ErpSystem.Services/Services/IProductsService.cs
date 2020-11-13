using System.Collections.Generic;
using ErpSystem.Services.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.Services
{
    public interface IProductsService
    {
        void CreateProduct(CreateProductViewModel createProduct);

        IEnumerable<ProductViewModel> SearchByProductNameAndId(int? productId, string productName);

        IEnumerable<ProductViewModel> SearchByProductPrice(decimal? minPrice, decimal? maxPrice);

        IEnumerable<ProductViewModel> SearchByProductSupplierCountryOrCity(string country, string city);

        IEnumerable<ProductViewModel> OrderProductsByGrossMargin();

        //IEnumerable<ProductViewModel> GetAvailableProducts();

        void DeleteExistingProduct(int productId, string productName);

        IEnumerable<SelectListItem> ProductTransportsPackageTags();

        IEnumerable<SelectListItem> ProductMeasurementTags();
    }
}
