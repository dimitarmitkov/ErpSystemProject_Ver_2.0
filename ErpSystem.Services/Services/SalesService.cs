using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.Sale;

namespace ErpSystem.Services.Services
{
    public class SalesService : ISalesService
    {
        private readonly ErpSystemDbContext dbContext;

        public SalesService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateSale(int productId, string customerId, int numberOfSoldProducts, bool hasProductDiscount, bool hasCustomerDiscount, int warehouesId)
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

            var productDiscount = this.dbContext.Products.Where(p => p.Id == productId).Select(x => x.ProductDiscount).FirstOrDefault();
            var price = this.dbContext.Products.Where(p => p.Id == productId).Select(x => x.ProductSalePrice).FirstOrDefault();
            var customerDiscount = this.dbContext.Customers.Where(c => c.Id == customerId).Select(x => x.CustomerDiscount).FirstOrDefault();


            if (hasProductDiscount && !string.IsNullOrEmpty(productDiscount.ToString())) sale.SingleProudctSalePrice = (decimal)(price - price * productDiscount / 100);
            else sale.SingleProudctSalePrice = (decimal)(price);

            if (hasCustomerDiscount && !string.IsNullOrEmpty(customerDiscount.ToString())) sale.SingleProudctSalePrice = (decimal)(sale.SingleProudctSalePrice - sale.SingleProudctSalePrice * customerDiscount / 100);

            var productSold = this.dbContext.WarehouseProducts.FirstOrDefault(p => p.ProductId == productId && p.ProductsAvailable != 0);

            if (productSold.ProductsAvailable >= numberOfSoldProducts)
            {

                //save sale in Sales database
                this.dbContext.Sales.Add(sale);
                int saved = this.dbContext.SaveChanges();


                //decrease number of available products based on number of sold products
                productSold.ProductsAvailable -= numberOfSoldProducts;

                this.dbContext.WarehouseProducts.Update(productSold);
                this.dbContext.SaveChanges();

                //decrase number of boxes or pallets if number of sold products reaches box or pallet size

                bool isProductInPallet = this.dbContext.Products.Where(p => p.Id == productSold.ProductId).Select(p => p.IsPallet == true).FirstOrDefault();
                var productSoldProduct = this.dbContext.Products.FirstOrDefault(p => p.Id == productId);
                var productSoldWarehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.Id == warehouesId);

                var numberOfProductsPerBox = this.dbContext.Products.Where(p => p.Id == productId).Select(x => x.ProductTransportPackageNumberOfPieces).FirstOrDefault();
                var boxesPerPallet = this.dbContext.Products.Where(p => p.Id == productId).Select(x => x.BoxesPerPallet).FirstOrDefault();
                var numberOfProductPerPallet = numberOfProductsPerBox * boxesPerPallet;

                //var currentNumberOfBoxesOrPallets = isProductInPallet ? productSold.ProductsAvailable / numberOfProductPerPallet : productSold.ProductsAvailable / numberOfProductsPerBox;

                //var preSaleNumberOfBoxesOrPallets = isProductInPallet ? (productSold.ProductsAvailable + numberOfSoldProducts) / numberOfProductPerPallet : (productSold.ProductsAvailable + numberOfSoldProducts) / numberOfProductsPerBox;

                var preSaleProductsAvailabe = productSold.ProductsAvailable + numberOfSoldProducts;

                //TODO transaction for sale

                if (isProductInPallet)
                {
                    //calculate pallets left
                    var numberOfSoldPallets = numberOfSoldProducts / numberOfProductPerPallet;
                    var partialPalletAdd = (productSold.ProductsAvailable % numberOfProductPerPallet) > 0 ? 1 : 0;
                    var numberOfPalletsLeft = (productSold.ProductsAvailable / numberOfProductPerPallet) + partialPalletAdd;

                    //calculate pallets before sale
                    var preSalePratialPalletAdd = (preSaleProductsAvailabe % numberOfProductPerPallet) > 0 ? 1 : 0;
                    var preSaleNumberOfPallets = (preSaleProductsAvailabe / numberOfProductPerPallet) + preSalePratialPalletAdd;

                    //increase space in warehouse if whole pallet was finished, if not - space remains unchanged
                    productSoldWarehouse.CurrentPalletsSpaceFree += preSaleNumberOfPallets - numberOfPalletsLeft;

                    this.dbContext.Warehouses.Update(productSoldWarehouse);
                    this.dbContext.SaveChanges();
                }

                if (!isProductInPallet)
                {
                    //get box size and shelf width size
                    var boxFront = this.dbContext.Products.Where(p => p.Id == productSold.ProductId).Select(x => x.ProductTransportPackageWidthSize).FirstOrDefault();
                    var boxLenght = this.dbContext.Products.Where(p => p.Id == productSold.ProductId).Select(x => x.ProductTransportPackageLengthSize).FirstOrDefault();
                    var shelfDepth = this.dbContext.WarehouseBoxes.Where(w => w.Id == productSold.WarehouseId).Select(x => x.ShelfDepth).FirstOrDefault();

                    //calculate boxes left
                    var numberOfSoldBoxes = numberOfSoldProducts / numberOfProductsPerBox;
                    var partialBoxAdd = (productSold.ProductsAvailable % numberOfProductsPerBox) > 0 ? 1 : 0;
                    var numberOfBoxesLeft = (productSold.ProductsAvailable / numberOfProductsPerBox) + partialBoxAdd;

                    //calculate number of boxes before sale
                    var preSalePratialBoxAdd = (preSaleProductsAvailabe % numberOfProductsPerBox) > 0 ? 1 : 0;
                    var preSaleNumberOfBoxes = (preSaleProductsAvailabe / numberOfProductsPerBox) + preSalePratialBoxAdd;
                    var boxes = preSaleNumberOfBoxes - numberOfBoxesLeft;

                    //increase space in warehouse if whole box was finished, if not - space remains unchanged
                    productSoldWarehouse.CurrentBoxesFrontSpaceFree += boxLenght > shelfDepth ? (boxes) * boxLenght : (boxes) * boxFront;

                    this.dbContext.Warehouses.Update(productSoldWarehouse);
                    this.dbContext.SaveChanges();
                }
            }
        }

        public IEnumerable<SaleViewModel> ListOfSales()
        {
            return this.dbContext.Sales.Select(x => new SaleViewModel
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

        //public Dictionary<string, decimal> TotalSalesPerDate()
        //{
        //    var listOfSales =
        //   this.dbContext.Sales.Select(s => new SaleSumByDateViewModel
        //   {
        //       DateOfSale = s.SaleDate,
        //       TotalSalesSum = s.SingleProudctSalePrice,
        //   }).ToList();

        //    var dictionary = new Dictionary<string, decimal>();

        //    foreach (var sale in listOfSales)
        //    {
        //        if (!dictionary.ContainsKey(sale.DateOfSale.Date.ToString()))
        //        {
        //            dictionary[sale.DateOfSale.Date.ToString()] = 0;
        //        }
        //        dictionary[sale.DateOfSale.Date.ToString()] += sale.TotalSalesSum;
        //    }

        //    return dictionary;
        //}

        public IDictionary<SaleSumByDateViewModel, SaleSumByDateViewModel> TotalSalesPerDate()
        {
            var listOfSales =
           this.dbContext.Sales.Select(s => new SaleSumByDateViewModel
           {
               DateOfSale = s.SaleDate,
               TotalSalesSum = s.SingleProudctSalePrice,
           }).ToList();

            var dictionary = new Dictionary<string, decimal>();

            foreach (var sale in listOfSales)
            {
                if (!dictionary.ContainsKey(sale.DateOfSale.Date.ToString()))
                {
                    dictionary[sale.DateOfSale.Date.ToString()] = 0;
                }
                dictionary[sale.DateOfSale.Date.ToString()] += sale.TotalSalesSum;
            }

            return dictionary;
        }
    }
}
