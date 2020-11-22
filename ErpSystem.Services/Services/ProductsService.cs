using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.Services
{
    public class ProductsService : IProductsService
    {
        private ErpSystemDbContext dbContext;

        public ProductsService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //create product service
        public void CreateProduct(CreateProductViewModel createProduct)
        {
            var product = new Product
            {
                ProductIndentificationNumber = createProduct.ProductIndentificationNumber,
                ProductName = createProduct.ProductName,
                ProductLandedPrice = createProduct.ProductLendedPrice,
                ProductGrossMargin = createProduct.ProductGrossMargin,
                TimeToOrder = createProduct.TimeToOrder,
                TimeToDelivery = createProduct.TimeToDelivery,
                IsPallet = bool.Parse(createProduct.IsPallet),
                ProductTransportPackageWidthSize = createProduct.ProductTransportPackageWidthSize,
                ProductTransportPackageLengthSize = createProduct.ProductTransportPackageWidthSize,
                ProductTransportPackageHeightSize = createProduct.ProductTransportPackageHeightSize,
                ProductTransportPackageWeight = createProduct.ProductTransportPackageWeight,
                ProductTransportPackageNumberOfPieces = createProduct.ProductTransportPackageNumberOfPieces,
                BoxesPerPallet = createProduct.BoxesPerPallet,
                SingleProductSize = createProduct.SingleProductSize,
                ProductDescription = createProduct.ProductDescription

            };

            //setting production date to null, if not set
            if (string.IsNullOrWhiteSpace(product.ProductionDate.ToString()))
            {
                product.ProductionDate = null;
            }
            else
            {
                product.ProductionDate = DateTime.Parse(createProduct.ProductionDate);
            }

            //setting expire date to null, if not set
            if (string.IsNullOrWhiteSpace(product.ExpireDate.ToString()))
            {
                product.ExpireDate = null;
            }
            else
            {
                product.ExpireDate = DateTime.Parse(createProduct.ExpireDate);
            }

            //setting of supplier
            var supplierEntity = this.dbContext.Suppliers.FirstOrDefault(s => s.SupplierName == createProduct.Supplier);

            product.Supplier = supplierEntity;

            //setting of productTransportPackage
            var productTransportPackageEntity = this.dbContext.TransportPackageTags.FirstOrDefault(tp => tp.TypeOfPackage == createProduct.ProductTransportPackage);

            if (productTransportPackageEntity == null)
            {
                productTransportPackageEntity = new TransportPackageTag
                {
                    TypeOfPackage = createProduct.ProductTransportPackage
                };
            }

            product.ProductTransportPackage = productTransportPackageEntity;

            //setting measurmentTag
            var measurmentTagEntity = this.dbContext.ProductMeasurmentTags.FirstOrDefault(m => m.Maesurment == createProduct.MeasurmentTag);

            if (measurmentTagEntity == null)
            {
                measurmentTagEntity = new ProductMeasurmentTag
                {
                    Maesurment = createProduct.MeasurmentTag
                };
            }

            product.MeasurmentTag = measurmentTagEntity;

            var resultativeGrossMargin = (decimal)product.ProductGrossMargin / 100;

            product.ProductSalePrice = product.ProductLandedPrice / (1 - resultativeGrossMargin);

            this.dbContext.Products.Add(product);
            this.dbContext.SaveChanges();
        }

        // delete product service
        public void DeleteExistingProduct(int productId, string productName)
        {
            var productDelete = this.dbContext.Products.FirstOrDefault(p => p.Id == productId && p.ProductName == productName);

            this.dbContext.Products.Remove(productDelete);
            this.dbContext.SaveChanges();
        }

        // serch by name or Id
        public IEnumerable<ProductViewModel> SearchByProductNameAndId(int? productId, string productName)
        {
            IQueryable<Product> productView = null;

            if (productId != null) productView = this.dbContext.Products.Where(p => p.Id == productId);
            if (productName != null) productView = this.dbContext.Products.Where(p => p.ProductName == productName);
            if (productName == null && productId == null) productView = this.dbContext.Products.Where(p => p.Id > -1);



            List<ProductViewModel> result = SelectProductViewModel(productView);

            return result;
        }


        // search by price
        public IEnumerable<ProductViewModel> SearchByProductPrice(decimal? minPrice, decimal? maxPrice)
        {
            IQueryable<Product> productView = null;

            if (minPrice != null && maxPrice == null) productView = this.dbContext.Products.Where(p => p.ProductSalePrice >= minPrice);
            else if (maxPrice != null && minPrice == null) productView = this.dbContext.Products.Where(p => p.ProductSalePrice <= maxPrice);
            else if (maxPrice != null && minPrice != null) productView = this.dbContext.Products.Where(p => p.ProductSalePrice >= minPrice && p.ProductSalePrice <= maxPrice);

            List<ProductViewModel> result = SelectProductViewModel(productView);

            return result;
        }


        // search product by country of origin
        public IEnumerable<ProductViewModel> SearchByProductSupplierCountryOrCity(string country, string city)
        {
            IQueryable<Product> productView = null;

            if (country != null && city == null) productView = this.dbContext.Products.Where(p => p.Supplier.SupplierCountry == country);
            else if (city != null && country == null) productView = this.dbContext.Products.Where(p => p.Supplier.SupplierAddress == city);
            else if (city != null && country != null) productView = this.dbContext.Products.Where(p => p.Supplier.SupplierCountry == country && p.Supplier.SupplierAddress == city);


            List<ProductViewModel> result = SelectProductViewModel(productView);

            return result;
        }

        public IEnumerable<SelectListItem> ProductTransportsPackageTags()
        {
            return this.dbContext.TransportPackageTags.Select(p => new SelectListItem
            {
                Text = p.TypeOfPackage,
                Value = p.TypeOfPackage,

            }).ToList();
        }

        public IEnumerable<SelectListItem> ProductMeasurementTags()
        {
            return this.dbContext.ProductMeasurmentTags.Select(p => new SelectListItem
            {
                Text = p.Maesurment,
                Value = p.Maesurment,

            }).ToList();
        }




        private List<ProductViewModel> SelectProductViewModel(IQueryable<Product> productView)
        {

            var listOfProductViewModel = productView.Select(x => new ProductViewModel
            {
                ProductName = x.ProductName,
                ProductLandedPrice = x.ProductLandedPrice,
                ProductSalePrice = x.ProductSalePrice,
                ProductGrossMargin = x.ProductGrossMargin,
                Supplier = x.Supplier.SupplierName,
                TimeToOrder = x.TimeToOrder,
                TimeToDelivery = x.TimeToDelivery,
                ProductTransportPackage = x.ProductTransportPackage.TypeOfPackage,
                MeasurmentTag = x.MeasurmentTag.Maesurment,
                ProductsAvailable = x.WarehouseProduct.ProductsAvailable,
                TotalProductsDeliveryPrice = x.WarehouseProduct.ProductsAvailable * x.ProductLandedPrice,
                ProductExpireDate = !string.IsNullOrEmpty(x.WarehouseProduct.ExpireDate.ToString())
                ? x.WarehouseProduct.ExpireDate.ToString() : "no expire date",
            }).OrderByDescending(x => x.ProductName)
                            .ToList();


            return listOfProductViewModel;
        }
    }
}
