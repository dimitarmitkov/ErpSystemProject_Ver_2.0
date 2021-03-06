﻿namespace ErpSystem.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services.ViewModels.Order;
    using ErpSystem.Services.ViewModels.Sale;
    using ErpSystem.Services.ViewModels.Warehouse;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SalesService : ISalesService
    {
        private readonly ErpSystemDbContext dbContext;

        public SalesService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // create sale start, test completed
        public void CreateSale(int productId, string customerId, int numberOfSoldProducts, bool hasProductDiscount, bool hasCustomerDiscount, int warehouseId, int productByExpireDateId)
        {
            var sale = new Sale
            {
                ProductId = productId,
                CustomerId = customerId,
                Customer = this.dbContext.Customers.FirstOrDefault(c => c.Id == customerId),
                Product = this.dbContext.Products.FirstOrDefault(p => p.Id == productId),
                SaleDate = DateTime.UtcNow,
                NumberOfSoldProducts = numberOfSoldProducts,
                HasCustomerDiscount = hasCustomerDiscount,
                HasProductDiscount = hasProductDiscount,
            };

            var productDiscount = this.dbContext.Products.Where(p => p.Id == productId && p.IsDeleted == false).Select(x => x.ProductDiscount).FirstOrDefault();
            var price = this.dbContext.Products.Where(p => p.Id == productId && p.IsDeleted == false).Select(x => x.ProductSalePrice).FirstOrDefault();
            var customerDiscount = this.dbContext.Customers.Where(c => c.Id == customerId).Select(x => x.CustomerDiscount).FirstOrDefault();

            if (hasProductDiscount && !string.IsNullOrEmpty(productDiscount.ToString()))
            {
                sale.SingleProudctSalePrice = (decimal)(price - (price * productDiscount / 100));
            }
            else
            {
                sale.SingleProudctSalePrice = (decimal)price;
            }

            if (hasCustomerDiscount && !string.IsNullOrEmpty(customerDiscount.ToString()))
            {
                sale.SingleProudctSalePrice = (decimal)(sale.SingleProudctSalePrice - (sale.SingleProudctSalePrice * customerDiscount / 100));
            }

            var productSold = this.dbContext.WarehouseProducts.FirstOrDefault(p => p.ProductId == productId && p.ProductsAvailable != 0 && p.Id == productByExpireDateId);

            if (productSold.ProductsAvailable >= numberOfSoldProducts)
            {
                // save sale in Sales database
                this.dbContext.Sales.Add(sale);
                int saved = this.dbContext.SaveChanges();

                // decrease number of available products based on number of sold products
                productSold.ProductsAvailable -= numberOfSoldProducts;

                this.dbContext.WarehouseProducts.Update(productSold);
                this.dbContext.SaveChanges();

                // check if product needs order
                this.IsProductForOrder(sale.ProductId);

                // decrase number of boxes or pallets if number of sold products reaches box or pallet size
                bool isProductInPallet = this.dbContext.Products.Where(p => p.Id == productSold.ProductId && p.IsDeleted == false).Select(p => p.IsPallet == true).FirstOrDefault();
                var productSoldProduct = this.dbContext.Products.FirstOrDefault(p => p.Id == productId && p.IsDeleted == false);
                var productSoldWarehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.Id == warehouseId);

                var numberOfProductsPerBox = this.dbContext.Products.Where(p => p.Id == productId && p.IsDeleted == false).Select(x => x.ProductTransportPackageNumberOfPieces).FirstOrDefault();
                var boxesPerPallet = this.dbContext.Products.Where(p => p.Id == productId && p.IsDeleted == false).Select(x => x.BoxesPerPallet).FirstOrDefault();
                var numberOfProductPerPallet = numberOfProductsPerBox * boxesPerPallet;

                var preSaleProductsAvailabe = productSold.ProductsAvailable + numberOfSoldProducts;

                // TODO transaction for sale
                if (isProductInPallet)
                {
                    // calculate pallets left
                    var numberOfSoldPallets = numberOfSoldProducts / numberOfProductPerPallet;
                    var partialPalletAdd = (productSold.ProductsAvailable % numberOfProductPerPallet) > 0 ? 1 : 0;
                    var numberOfPalletsLeft = (productSold.ProductsAvailable / numberOfProductPerPallet) + partialPalletAdd;

                    // calculate pallets before sale
                    var preSalePratialPalletAdd = (preSaleProductsAvailabe % numberOfProductPerPallet) > 0 ? 1 : 0;
                    var preSaleNumberOfPallets = (preSaleProductsAvailabe / numberOfProductPerPallet) + preSalePratialPalletAdd;

                    // increase space in warehouse if whole pallet was finished, if not - space remains unchanged
                    productSoldWarehouse.CurrentPalletsSpaceFree += preSaleNumberOfPallets - numberOfPalletsLeft;

                    this.dbContext.Warehouses.Update(productSoldWarehouse);
                    this.dbContext.SaveChanges();
                }

                if (!isProductInPallet)
                {
                    // get box size and shelf width size
                    var boxFront = this.dbContext.Products.Where(p => p.Id == productSold.ProductId && p.IsDeleted == false).Select(x => x.ProductTransportPackageWidthSize).FirstOrDefault();
                    var boxLenght = this.dbContext.Products.Where(p => p.Id == productSold.ProductId && p.IsDeleted == false).Select(x => x.ProductTransportPackageLengthSize).FirstOrDefault();
                    var shelfDepth = this.dbContext.WarehouseBoxes.Where(w => w.Id == productSold.WarehouseId).Select(x => x.ShelfDepth).FirstOrDefault();

                    // calculate boxes left
                    var numberOfSoldBoxes = numberOfSoldProducts / numberOfProductsPerBox;
                    var partialBoxAdd = (productSold.ProductsAvailable % numberOfProductsPerBox) > 0 ? 1 : 0;
                    var numberOfBoxesLeft = (productSold.ProductsAvailable / numberOfProductsPerBox) + partialBoxAdd;

                    // calculate number of boxes before sale
                    var preSalePratialBoxAdd = (preSaleProductsAvailabe % numberOfProductsPerBox) > 0 ? 1 : 0;
                    var preSaleNumberOfBoxes = (preSaleProductsAvailabe / numberOfProductsPerBox) + preSalePratialBoxAdd;
                    var boxes = preSaleNumberOfBoxes - numberOfBoxesLeft;

                    // increase space in warehouse if whole box was finished, if not - space remains unchanged
                    productSoldWarehouse.CurrentBoxesFrontSpaceFree += boxLenght > shelfDepth ? boxes * boxLenght : boxes * boxFront;

                    this.dbContext.Warehouses.Update(productSoldWarehouse);
                    this.dbContext.SaveChanges();
                }
            }
        } // create sale end

        // list of all sales, in use sales controller All(), test completed
        public IEnumerable<SalesPerCustomerOrProductViewModel> ListOfAllSales()
        {
            return this.dbContext.Sales.Select(x => new SalesPerCustomerOrProductViewModel
            {
                Product = x.Product.ProductName,
                Customer = x.Customer.CompanyName,
                SaleDate = x.SaleDate,
                SingleProudctSalePrice = x.SingleProudctSalePrice,
                NumberOfSoldProducts = x.NumberOfSoldProducts,
                TotalSalePrice = x.NumberOfSoldProducts * x.SingleProudctSalePrice,
                ProductMesure = x.Product.MeasurmentTag.Maesurment,
            }).ToList();
        }

        // in use - sales controller WarehouseAll(GET,POST), test completed
        public IEnumerable<WarehouseProductViewModel> ListOfProductsForSale()
        {
            return this.dbContext.WarehouseProducts.Where(wp => wp.ProductsAvailable > 0).Select(x => new WarehouseProductViewModel
            {
                Id = x.Id,
                Product = x.Product.ProductName,
                WarehouseId = x.WarehouseId,
                ProductMeasurement = x.Product.MeasurmentTag.Maesurment,
                ProductExpireDate = x.ExpireDate.ToString(),
                ProductsAvailable = x.ProductsAvailable,
            }).OrderBy(p => p.Product)
            .ThenByDescending(p => p.ProductExpireDate)
                .ToList();
        }

        // generates crud record for customer, test completed
        public void GenerateCurrentSale(string companyEik, bool hasDiscount, string userId)
        {
            // TODO add userID for confirmation
            var deletableCustomer = this.dbContext.CurrentSales.FirstOrDefault(c => c.Id > -1);

            this.dbContext.CurrentSales.Remove(deletableCustomer);
            this.dbContext.SaveChanges();

            var customerId = this.dbContext.Customers.Where(c => c.CompanyEik == companyEik).Select(x => x.Id).FirstOrDefault();
            var customerName = this.dbContext.Customers.Where(c => c.CompanyEik == companyEik).Select(x => x.CompanyName).FirstOrDefault();

            var current = new CurrentSale
            {
                CustomerId = customerId,
                CustomerEikNumber = companyEik,
                CustomerName = customerName,
                HasCustomerDiscount = hasDiscount,
            };

            this.dbContext.CurrentSales.Add(current);
            this.dbContext.SaveChanges();
        }

        // in use - sales controller WarehouseAll(GET,POST)
        public IEnumerable<SelectListItem> SeclectCustomerDropDown()
        {
            return this.dbContext.Customers.Select(p => new SelectListItem
            {
                Text = p.CompanyName + " " + p.CompanyTypeOfRegistration.CompanyTypeOfRegistration + " " + p.CompanyEik,
                Value = p.CompanyEik,
            }).ToList();
        }

        // test cmpleted
        public IEnumerable<WarehouseProductViewModel> ListOfProductsForSaleWithCustomer()
        {
            var customerName = this.dbContext.CurrentSales.Select(x => x.CustomerName).FirstOrDefault();
            var customerId = this.dbContext.CurrentSales.Select(x => x.CustomerId).FirstOrDefault();
            var hasDiscount = this.dbContext.CurrentSales.Select(x => x.HasCustomerDiscount).FirstOrDefault();

            return this.dbContext.WarehouseProducts.Where(wp => wp.ProductsAvailable > 0).Select(x => new WarehouseProductViewModel
            {
                Id = x.Id,
                Product = x.Product.ProductName,
                ProductId = x.ProductId,
                WarehouseId = x.WarehouseId,
                ProductMeasurement = x.Product.MeasurmentTag.Maesurment,
                ProductExpireDate = x.ExpireDate.ToString(),
                ProductsAvailable = x.ProductsAvailable,
                CustomerName = customerName,
                HasCustomerDiscount = hasDiscount,
                WarehouseProductId = x.Id,
                CustomerId = customerId,
            }).OrderBy(p => p.Product)
            .ThenByDescending(p => p.ProductExpireDate)
                .ToList();
        }

        // test completed
        public IEnumerable<SalesPerCustomerOrProductViewModel> ListOfSales(string customerName, string productName)
        {
            IQueryable<Sale> saleView = null;

            if (!string.IsNullOrEmpty(customerName))
            {
                saleView = this.dbContext.Sales.Where(s => s.Customer.CompanyName == customerName);
            }
            else if (!string.IsNullOrEmpty(customerName) && !string.IsNullOrEmpty(productName))
            {
                saleView = this.dbContext.Sales.Where(s => s.Customer.CompanyName == customerName && s.Product.ProductName == productName);
            }
            else if (!string.IsNullOrEmpty(productName))
            {
                saleView = this.dbContext.Sales.Where(s => s.Product.ProductName == productName);
            }
            else
            {
                saleView = this.dbContext.Sales.Where(s => !string.IsNullOrEmpty(s.Id.ToString()));
            }

            List<SalesPerCustomerOrProductViewModel> listOfSales = this.ListOfSalesAsSalesViewModel(saleView);
            return listOfSales;
        }

        // collection of sales calculating total sales per day, retursn dictionary of date and amount, test completed
        public IEnumerable<KeyValuePair<string, decimal>> TotalSalesPerDate()
        {
            var listOfSales = this.dbContext.Sales.Select(s => new SaleSumByDateViewModel
            {
                DateOfSale = s.SaleDate,
                TotalSalesSum = s.SingleProudctSalePrice * s.NumberOfSoldProducts,
            }).ToList();

            var dictionary = new Dictionary<string, decimal>();

            foreach (var sale in listOfSales)
            {
                if (!dictionary.ContainsKey(sale.DateOfSale.Date.ToString("yyyy-MM-dd")))
                {
                    dictionary[sale.DateOfSale.Date.ToString("yyyy-MM-dd")] = 0;
                }

                dictionary[sale.DateOfSale.Date.ToString("yyyy-MM-dd")] += sale.TotalSalesSum;
            }

            return dictionary;
        }

        // calculating does product need an order, test completed
        public void IsProductForOrder(int currentId)
        {
            var productForOrder = this.dbContext.Products.Where(p => p.Id == currentId && p.IsDeleted == false).Select(x => new DeliveryNeededProduct
            {
                Product = x.ProductName,
                ProductId = currentId,
                ProductsAvailable = this.dbContext.WarehouseProducts.Where(p => p.ProductId == currentId).Sum(x => x.ProductsAvailable),
                SalesBasedOnDeliveryPeriod = this.dbContext.Sales.Where(p => p.ProductId == currentId && p.SaleDate >= DateTime.UtcNow.AddDays((x.TimeToDelivery + x.TimeToOrder) * (-1))).Sum(x => x.NumberOfSoldProducts),
                Supplier = x.Supplier.SupplierName,
                DeliveryDays = x.TimeToDelivery,
                OrderDays = x.TimeToOrder,
                TotalDeliveryTime = x.TimeToOrder + x.TimeToDelivery,
                ConfimBeenNoticed = false,
            }).FirstOrDefault();

            var isProductAlreadySetForOrder = !this.dbContext.DeliveryNeededProducts.Any(p => p.ProductId == currentId);

            if (productForOrder.SalesBasedOnDeliveryPeriod >= productForOrder.ProductsAvailable && isProductAlreadySetForOrder)
            {
                this.dbContext.DeliveryNeededProducts.Add(productForOrder);
                this.dbContext.SaveChanges();
            }
        }

        // test completed
        public IEnumerable<CalculateNeedOfOrderViewModel> AreAnyProductsForOrder()
        {
            return this.dbContext.DeliveryNeededProducts.Where(p => p.ConfimBeenNoticed == false).Select(x => new CalculateNeedOfOrderViewModel
            {
                Product = x.Product,
                ProductId = x.ProductId,
                Supplier = x.Supplier,
                DeliveryDays = x.OrderDays,
                OrderDays = x.DeliveryDays,
            }).ToList();
        }

        public void ConfirmNeedOfOrder(DeliveryNeededProduct deliveryNeededProduct)
        {
            var productId = deliveryNeededProduct.ProductId;
            var confirmedProducts = this.dbContext.DeliveryNeededProducts.Where(p => p.ConfimBeenNoticed == false).ToList();

            for (int i = 0; i < confirmedProducts.Count; i++)
            {
                confirmedProducts[i].ConfimBeenNoticed = true;
                this.dbContext.Update(confirmedProducts[i]);
                this.dbContext.SaveChanges();
            }
        }

        public IEnumerable<InvoiceViewModel> Invoice()
        {
            var customerId = this.dbContext.CurrentSales.Select(x => x.CustomerId).FirstOrDefault();

            var list = this.dbContext.Sales.Where(s => s.CustomerId == customerId && s.SaleDate.Date == DateTime.UtcNow.Date).Select(x => new InvoiceViewModel
            {
                Customer = x.Customer.CompanyName,
                CustomerTypeOfRegistration = x.Customer.CompanyTypeOfRegistration.CompanyTypeOfRegistration,
                SaleDate = DateTime.UtcNow.Date,
                Eik = x.Customer.CompanyEik,
                Address = x.Customer.Address + ", " + x.Customer.PostalCode + " " + x.Customer.City,
                Email = x.Customer.Email,
                Phone = x.Customer.PhoneNumber,
                Product = x.Product.ProductName,
                ProductMesure = x.Product.MeasurmentTag.Maesurment,
                ProductDiscount = x.HasProductDiscount ? x.Product.ProductDiscount : 0,
                CustomerDiscount = x.HasCustomerDiscount ? x.Customer.CustomerDiscount : 0,
                SingleProudctSalePrice = x.SingleProudctSalePrice,
                NumberOfSoldProducts = x.NumberOfSoldProducts,
                TotalSalePrice = x.NumberOfSoldProducts * x.SingleProudctSalePrice,
            }).ToList();

            return list;
        }

        // private
        private List<SalesPerCustomerOrProductViewModel> ListOfSalesAsSalesViewModel(IQueryable<Sale> saleView)
        {
            return saleView.Select(x => new SalesPerCustomerOrProductViewModel
            {
                Product = x.Product.ProductName,
                Customer = x.Customer.CompanyName,
                SaleDate = x.SaleDate,
                SingleProudctSalePrice = x.SingleProudctSalePrice,
                NumberOfSoldProducts = x.NumberOfSoldProducts,
                TotalSalePrice = x.NumberOfSoldProducts * x.SingleProudctSalePrice,
                ProductMesure = x.Product.MeasurmentTag.Maesurment,
            }).ToList();
        }
    }
}
