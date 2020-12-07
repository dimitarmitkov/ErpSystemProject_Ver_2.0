using System;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Areas.Administration.Controllers
{
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
            viewModel.ProductList = productsService.SearchByProductNameAndId(null, null);

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(CombinedProductViewModel combinedProductView)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new CombinedProductViewModel();
                viewModel.ProductList = productsService.SearchByProductNameAndId(null, null);

                return this.View(viewModel);
            }

            productsService.DeleteExistingProduct(combinedProductView.ProductSingle.ProductId, combinedProductView.ProductSingle.ProductName);

            return this.Redirect("Administration/ProductsEdit/Delete");
        }

    }
}
