using System;
using System.Collections.Generic;
using System.Linq;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }


        public IActionResult Search()
        {
            IEnumerable<ProductViewModel> viewModel = this.productsService.FindAll();
            return this.View(viewModel);
        }

        public IActionResult CreateProduct()
        {
            var viewModel = new CreateProductViewModel();
            viewModel.PackageItems = this.productsService.ProductTransportsPackageTags();
            viewModel.MeasureItems = this.productsService.ProductMeasurementTags();

            return this.View(viewModel);
        }


        [HttpPost]
        public IActionResult CreateProduct(CreateProductViewModel createProduct)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CreateProductViewModel();
                viewModel.PackageItems = this.productsService.ProductTransportsPackageTags();
                viewModel.MeasureItems = this.productsService.ProductMeasurementTags();

                return this.View(viewModel);
            }

            this.productsService.CreateProduct(createProduct);
            return this.Redirect("/Products/Search");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete()
        {
            var viewModel = new CombinedProductViewModel();
            viewModel.ProductList = productsService.SearchByProductNameAndId(null, null);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(CombinedProductViewModel combinedProductView)
        {


            if (!ModelState.IsValid)
            {
                var viewModel = new CombinedProductViewModel();
                viewModel.ProductList = productsService.SearchByProductNameAndId(null, null);

                return this.View(viewModel);
            }

            productsService.DeleteExistingProduct(combinedProductView.ProductSingle.ProductId, combinedProductView.ProductSingle.ProductName);

            return this.Redirect("/Products/Delete");
        }



    }
}
