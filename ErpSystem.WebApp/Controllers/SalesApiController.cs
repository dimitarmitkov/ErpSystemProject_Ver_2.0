﻿using System;
using System.Collections.Generic;
using ErpSystem.Services.Services;
using ErpSystem.Services.ViewModels.Sale;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesApiController : Controller
    {
        private readonly ISalesService salesService;

        public SalesApiController(ISalesService salesService)
        {
            this.salesService = salesService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult SaleRest([FromBody] SalesPerCustomerOrProductViewModel salesPerCustomer)
        {
            var customerName = salesPerCustomer.Customer;
            var productName = salesPerCustomer.Product;

            var viewModel = this.salesService.ListOfSales(customerName, productName);
            return this.Json(viewModel);
        }

        public IActionResult ReturnData()
        {
            var result = "name";
            var name = "first";

            return this.Json(result + " " + name);
        }
    }
}