namespace ErpSystem.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services.ViewModels.Product;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ProductsService : IProductsService
    {
        private ErpSystemDbContext dbContext;

        public ProductsService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // create product service, test done
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
                ProductTransportPackageLengthSize = createProduct.ProductTransportPackageLengthSize,
                ProductTransportPackageHeightSize = createProduct.ProductTransportPackageHeightSize,
                ProductTransportPackageWeight = createProduct.ProductTransportPackageWeight,
                ProductTransportPackageNumberOfPieces = createProduct.ProductTransportPackageNumberOfPieces,
                BoxesPerPallet = createProduct.BoxesPerPallet,
                SingleProductSize = createProduct.SingleProductSize,
                ProductDescription = createProduct.ProductDescription,
                ProductDiscount = createProduct.ProductDiscount,
            };

            // setting production date to null, if not set
            if (!string.IsNullOrWhiteSpace(createProduct.ProductionDate))
            {
                product.ProductionDate = DateTime.Parse(createProduct.ProductionDate);
            }
            else
            {
                product.ProductionDate = null;
            }

            // setting expire date to null, if not set
            if (!string.IsNullOrWhiteSpace(createProduct.ExpireDate))
            {
                product.ExpireDate = DateTime.Parse(createProduct.ExpireDate);
            }
            else
            {
                product.ExpireDate = null;
            }

            // setting of supplier
            var supplierEntity = this.dbContext.Suppliers.FirstOrDefault(s => s.SupplierName == createProduct.Supplier);

            product.Supplier = supplierEntity;

            // setting of productTransportPackage
            var productTransportPackageEntity = this.dbContext.TransportPackageTags.FirstOrDefault(tp => tp.TypeOfPackage == createProduct.ProductTransportPackage);

            if (productTransportPackageEntity == null)
            {
                productTransportPackageEntity = new TransportPackageTag
                {
                    TypeOfPackage = createProduct.ProductTransportPackage,
                };
            }

            product.ProductTransportPackage = productTransportPackageEntity;

            // setting measurmentTag
            var measurmentTagEntity = this.dbContext.ProductMeasurmentTags.FirstOrDefault(m => m.Maesurment == createProduct.MeasurmentTag);

            if (measurmentTagEntity == null)
            {
                measurmentTagEntity = new ProductMeasurmentTag
                {
                    Maesurment = createProduct.MeasurmentTag,
                };
            }

            product.MeasurmentTag = measurmentTagEntity;

            var resultativeGrossMargin = (decimal)product.ProductGrossMargin / 100;

            product.ProductSalePrice = product.ProductLandedPrice / (1 - resultativeGrossMargin);

            this.dbContext.Products.Add(product);
            this.dbContext.SaveChanges();
        }

        // delete product service
        public async Task DeleteExistingProduct(int productId, string productName)
        {
            var productDelete = this.dbContext.Products.FirstOrDefault(p => p.Id == productId && p.ProductName == productName && p.IsDeleted == false);

            productDelete.IsDeleted = true;
            this.dbContext.Products.Update(productDelete);
            await this.dbContext.SaveChangesAsync();
        }

        // find all products, test done
        public IEnumerable<ProductViewModel> FindAll()
        {
            var result = this.dbContext.Products.Where(p => p.IsDeleted == false).Select(x => new ProductViewModel
            {
                ProductName = x.ProductName,
                ProductId = x.Id,
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
                ProductExpireDate = x.WarehouseProduct.ExpireDate.ToString(),
            }).OrderByDescending(x => x.ProductName)
              .ToList();

            return result;
        }

        // serch by name or Id, test done
        public IEnumerable<ProductViewModel> SearchByProductNameAndId(int? productId, string productName)
        {
            IQueryable<Product> productView = null;

            if (productId != null)
            {
                productView = this.dbContext.Products.Where(p => p.Id == productId && p.IsDeleted == false);
            }

            if (productName != null)
            {
                productView = this.dbContext.Products.Where(p => p.ProductName == productName && p.IsDeleted == false);
            }

            if (productName == null && productId == null)
            {
                productView = this.dbContext.Products.Where(p => p.Id > -1 && p.IsDeleted == false);
            }

            List<ProductViewModel> result = this.SelectProductViewModel(productView);

            return result;
        }

        // search by price, test done
        public IEnumerable<ProductViewModel> SearchByProductPrice(decimal? minPrice, decimal? maxPrice)
        {
            IQueryable<Product> productView = null;

            if (minPrice != null && maxPrice == null)
            {
                productView = this.dbContext.Products.Where(p => p.ProductSalePrice >= minPrice && p.IsDeleted == false);
            }
            else if (maxPrice != null && minPrice == null)
            {
                productView = this.dbContext.Products.Where(p => p.ProductSalePrice <= maxPrice && p.IsDeleted == false);
            }
            else if (maxPrice != null && minPrice != null)
            {
                productView = this.dbContext.Products.Where(p => p.ProductSalePrice >= minPrice && p.ProductSalePrice <= maxPrice && p.IsDeleted == false);
            }

            List<ProductViewModel> result = this.SelectProductViewModel(productView);

            return result;
        }

        // search product by country of origin, test done
        public IEnumerable<ProductViewModel> SearchByProductSupplierCountryOrCity(string country, string city)
        {
            IQueryable<Product> productView = null;

            if (country != null && city == null)
            {
                productView = this.dbContext.Products.Where(p => p.Supplier.SupplierCountry == country && p.IsDeleted == false);
            }
            else if (city != null && country == null)
            {
                productView = this.dbContext.Products.Where(p => p.Supplier.SupplierAddress == city && p.IsDeleted == false);
            }
            else if (city != null && country != null)
            {
                productView = this.dbContext.Products.Where(p => p.Supplier.SupplierCountry == country && p.Supplier.SupplierAddress == city && p.IsDeleted == false);
            }
            else if (country == null && city == null)
            {
                productView = this.dbContext.Products.Where(p => p.IsDeleted == false);
            }

            List<ProductViewModel> result = this.SelectProductViewModel(productView);

            return result;
        }

        // product transport package tags
        public IEnumerable<SelectListItem> ProductTransportsPackageTags()
        {
            return this.dbContext.TransportPackageTags.Select(p => new SelectListItem
            {
                Text = p.TypeOfPackage,
                Value = p.TypeOfPackage,
            }).ToList();
        }

        // product measurements tag
        public IEnumerable<SelectListItem> ProductMeasurementTags()
        {
            return this.dbContext.ProductMeasurmentTags.Select(p => new SelectListItem
            {
                Text = p.Maesurment,
                Value = p.Maesurment,
            }).ToList();
        }

        // pivate method, included in tests
        private List<ProductViewModel> SelectProductViewModel(IQueryable<Product> productView)
        {
            var listOfProductViewModel = productView.Select(x => new ProductViewModel
            {
                ProductName = x.ProductName,
                ProductId = x.Id,
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
                ProductExpireDate = x.WarehouseProduct.ExpireDate.ToString(),
            }).OrderByDescending(x => x.ProductName)
              .ToList();

            return listOfProductViewModel;
        }
    }
}