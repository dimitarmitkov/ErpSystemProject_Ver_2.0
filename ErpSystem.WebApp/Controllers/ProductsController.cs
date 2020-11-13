using System;
using System.Collections.Generic;
using System.Linq;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Product;
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
            IEnumerable<ProductViewModel> viewModel = this.productsService.SearchByProductNameAndId(null, null);
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

    }
}
