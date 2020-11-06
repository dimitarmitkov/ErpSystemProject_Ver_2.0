using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreateSale(int productId, string customerId, int numberOfSoldProducts, bool hasproductDiscount, bool hasCustomerDiscount, int warehouesId)
        {
            var sale = new Sale
            {
                ProductId = productId,
                CustomerId = customerId,
                Customer = this.dbContext.Customers.FirstOrDefault(c => c.Id == customerId),
                Product = this.dbContext.Products.FirstOrDefault(p => p.Id == productId),
                SaleDate = DateTime.UtcNow,
                NumberOfSoldProducts = numberOfSoldProducts,
            };

            var productDiscount = this.dbContext.Products.Where(p => p.Id == productId).Select(x => x.ProductDiscount).FirstOrDefault();
            var price = this.dbContext.Products.Where(p => p.Id == productId).Select(x => x.ProductSalePrice).FirstOrDefault();
            var customerDiscount = this.dbContext.Customers.Where(c => c.Id == customerId).Select(x => x.CustomerDiscount).FirstOrDefault();


            if (hasproductDiscount && !string.IsNullOrEmpty(productDiscount.ToString())) sale.SingleProudctSalePrice = (decimal)(price - price * productDiscount / 100);
            else sale.SingleProudctSalePrice = (decimal)(price);

            if (hasCustomerDiscount && !string.IsNullOrEmpty(customerDiscount.ToString())) sale.SingleProudctSalePrice = (decimal)(sale.SingleProudctSalePrice - sale.SingleProudctSalePrice * customerDiscount / 100);


            this.dbContext.Sales.Add(sale);
            int saved = this.dbContext.SaveChanges();

            var productSold = this.dbContext.WarehouseProducts.FirstOrDefault(p => p.ProductId == productId);

            //decrease number of available products based on number of sold products
            if (!string.IsNullOrEmpty(saved.ToString()))
            {
                if (productSold.ProductsAvailable < numberOfSoldProducts)
                {
                    Console.WriteLine("Not enough products " + $"{numberOfSoldProducts - productSold.ProductsAvailable}" + " missing");

                    return;
                }

                productSold.ProductsAvailable -= numberOfSoldProducts;

                this.dbContext.WarehouseProducts.Update(productSold);
                this.dbContext.SaveChanges();
            }

            //decrase number of boxes or pallets if number of sold products reaches box or pallet size
            if (!string.IsNullOrEmpty(saved.ToString()))
            {
                bool isProductInPallet = this.dbContext.Products.Where(p => p.Id == productSold.ProductId).Select(p => p.IsPallet == true).FirstOrDefault();
                var productSoldProduct = this.dbContext.Products.FirstOrDefault(p => p.Id == productId);
                var productSoldWarehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.Id == warehouesId);

                var numberOfProductsPerBox = this.dbContext.Products.Where(p => p.Id == productId).Select(x => x.ProductTransportPackageNumberOfPieces).FirstOrDefault();
                var boxesPerPallet = this.dbContext.Products.Where(p => p.Id == productId).Select(x => x.BoxesPerPallet).FirstOrDefault();
                var numberOfProductPerPallet = numberOfProductsPerBox * boxesPerPallet;

                var currentNumberOfBoxesOrPallets = isProductInPallet ? productSold.ProductsAvailable / numberOfProductPerPallet : productSold.ProductsAvailable / numberOfProductsPerBox;

                var preSaleNumberOfBoxesOrPallets = isProductInPallet ? (productSold.ProductsAvailable + numberOfSoldProducts) / numberOfProductPerPallet : (productSold.ProductsAvailable + numberOfSoldProducts) / numberOfProductsPerBox;

                //TODO transaction for sale

                if (isProductInPallet && currentNumberOfBoxesOrPallets <= preSaleNumberOfBoxesOrPallets)
                {
                    if (productSold.ProductsAvailable == 0)
                    {
                        productSoldWarehouse.CurrentPalletsSpaceFree += 1;
                    }

                    if (preSaleNumberOfBoxesOrPallets == 1 && currentNumberOfBoxesOrPallets == 0 && productSold.ProductsAvailable != 0) preSaleNumberOfBoxesOrPallets = 0;

                    productSoldWarehouse.CurrentPalletsSpaceFree += preSaleNumberOfBoxesOrPallets - currentNumberOfBoxesOrPallets;

                    this.dbContext.Warehouses.Update(productSoldWarehouse);
                    this.dbContext.SaveChanges();
                }

                if (!isProductInPallet && currentNumberOfBoxesOrPallets <= preSaleNumberOfBoxesOrPallets)
                {
                    var boxFront = this.dbContext.Products.Where(p => p.Id == productSold.ProductId).Select(x => x.ProductTransportPackageWidthSize).FirstOrDefault();
                    var boxLenght = this.dbContext.Products.Where(p => p.Id == productSold.ProductId).Select(x => x.ProductTransportPackageLengthSize).FirstOrDefault();
                    var shelfDepth = this.dbContext.WarehouseBoxes.Where(w => w.Id == productSold.WarehouseId).Select(x => x.ShelfDepth).FirstOrDefault();

                    if (productSold.ProductsAvailable % numberOfProductsPerBox != 0)
                    {
                        productSoldWarehouse.CurrentBoxesFrontSpaceFree += 0;
                    }

                    else if (productSold.ProductsAvailable == 0)
                    {
                        productSoldWarehouse.CurrentBoxesFrontSpaceFree += boxLenght > shelfDepth ? boxLenght : boxFront;
                    }
                    else
                    {
                        productSoldWarehouse.CurrentBoxesFrontSpaceFree += boxLenght > shelfDepth ? (preSaleNumberOfBoxesOrPallets - currentNumberOfBoxesOrPallets) * boxLenght : (preSaleNumberOfBoxesOrPallets - currentNumberOfBoxesOrPallets) * boxFront;
                    }

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
    }
}
