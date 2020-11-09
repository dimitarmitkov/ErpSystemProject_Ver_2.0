﻿using System;
using System.Collections.Generic;
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
    }
}