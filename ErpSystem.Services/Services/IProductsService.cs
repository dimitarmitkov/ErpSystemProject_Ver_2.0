namespace ErpSystem.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ErpSystem.Services.ViewModels.Product;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IProductsService
    {
        void CreateProduct(CreateProductViewModel createProduct);

        IEnumerable<ProductViewModel> SearchByProductNameAndId(int? productId, string productName);

        IEnumerable<ProductViewModel> SearchByProductPrice(decimal? minPrice, decimal? maxPrice);

        IEnumerable<ProductViewModel> SearchByProductSupplierCountryOrCity(string country, string city);

        Task DeleteExistingProduct(int productId, string productName);

        IEnumerable<SelectListItem> ProductTransportsPackageTags();

        IEnumerable<SelectListItem> ProductMeasurementTags();

        IEnumerable<ProductViewModel> FindAll();
    }
}