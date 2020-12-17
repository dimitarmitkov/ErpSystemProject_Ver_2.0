namespace ErpSystem.WebApp.Areas.Administration.Controllers
{
    using ErpSystem.Services.Services;
    using ErpSystem.Services.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class ProductsEditController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsEditController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Delete()
        {
            var viewModel = new CombinedProductViewModel();
            viewModel.ProductList = this.productsService.SearchByProductNameAndId(null, null);

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(CombinedProductViewModel combinedProductView)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = new CombinedProductViewModel();
                viewModel.ProductList = this.productsService.SearchByProductNameAndId(null, null);

                return this.View(viewModel);
            }

            this.productsService.DeleteExistingProduct(combinedProductView.ProductSingle.ProductId, combinedProductView.ProductSingle.ProductName);

            return this.Redirect("Administration/ProductsEdit/Delete");
        }
    }
}
