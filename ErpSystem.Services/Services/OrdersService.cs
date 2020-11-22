using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpSystem.Data;
using ErpSystem.Services.ViewModels.Order;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ErpSystemDbContext dbContext;

        public OrdersService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task GenetareOrder()
        {

            var supplier = this.dbContext.DeliveryNeededProducts.Select(x => x.Supplier).FirstOrDefault();
            var prodictId = this.dbContext.Products.Where(p => p.Supplier.SupplierName == supplier).Select(x => x.Id).FirstOrDefault();
            var isPallet = this.dbContext.Products.Where(p => p.Id == prodictId).Select(x => x.IsPallet).FirstOrDefault();
            var piecesPerBox = this.dbContext.Products.Where(p => p.Id == prodictId).Select(x => x.ProductTransportPackageNumberOfPieces).FirstOrDefault();
            var boxesPerPallet = this.dbContext.Products.Where(p => p.Id == prodictId).Select(x => x.BoxesPerPallet).FirstOrDefault();
            var numberOfProductsInTransportUnit = isPallet ? piecesPerBox * boxesPerPallet : piecesPerBox;


            var order = new CalculateNeedOfOrderViewModel
            {
                Product = this.dbContext.Products.Where(p => p.Supplier.SupplierName == supplier).Select(x => x.ProductName).FirstOrDefault(),
                ProductId = prodictId,
                ProductsAvailable = this.dbContext.WarehouseProducts.Where(x => x.ProductId == prodictId).Sum(x => x.ProductsAvailable),
                SalesBasedOnDeliveryPeriod = this.dbContext.Sales.Where(p => p.ProductId == prodictId).Sum(x => x.NumberOfSoldProducts),
                ProductExwPrice = this.dbContext.Products.Where(p => p.Supplier.SupplierName == supplier).Select(x => x.ProductLandedPrice).FirstOrDefault(),
                ProductMeasurementType = this.dbContext.Products.Where(p => p.Id == prodictId).Select(x => x.MeasurmentTag.Maesurment).FirstOrDefault(),
                NumberOfProductsInTrasportUnit = numberOfProductsInTransportUnit,
                TotalWeightOfTransportUnit = this.dbContext.Products.Where(p => p.Id == prodictId).Select(x => x.ProductTransportPackageWeight).FirstOrDefault(),
            };

        }

        public IEnumerable<CalculateNeedOfOrderViewModel> ProductsForOrderList()
        {

            var supplier = this.dbContext.DeliveryNeededProducts.Select(s => s.Supplier).ToList();

            var productsList = new List<CalculateNeedOfOrderViewModel>();

            for (int i = 0; i < supplier.Count; i++)
            {
                var products = this.dbContext.Products.Where(s => s.Supplier.SupplierName == supplier[i]).Select(x => new CalculateNeedOfOrderViewModel
                {
                    ProductId = x.Id,
                    Product = x.ProductName,
                    Supplier = supplier[i],
                    TotalDeliveryTime = x.TimeToDelivery + x.TimeToOrder,
                    SalesBasedOnDeliveryPeriod = this.dbContext.Sales.Where(p => p.ProductId == x.Id && p.SaleDate >= DateTime.UtcNow.AddDays((x.TimeToDelivery + x.TimeToOrder) * (-1))).Sum(s => s.NumberOfSoldProducts),
                    ProductsAvailable = this.dbContext.WarehouseProducts.Where(p => p.ProductId == x.Id).Sum(x => x.ProductsAvailable),
                    ProductExwPrice = x.ProductLandedPrice,
                    ProductMeasurementType = x.MeasurmentTag.Maesurment,
                    NumberOfProductsInTrasportUnit = x.IsPallet ? x.ProductTransportPackageNumberOfPieces * x.BoxesPerPallet : x.ProductTransportPackageNumberOfPieces,
                    TotalWeightOfTransportUnit = x.ProductTransportPackageWeight * (x.IsPallet ? x.ProductTransportPackageNumberOfPieces * x.BoxesPerPallet : x.ProductTransportPackageNumberOfPieces),
                    NumberOfTransportUnitsClaculatedForOrder = (this.dbContext.Sales.Where(p => p.ProductId == x.Id && p.SaleDate >= DateTime.UtcNow.AddDays((x.TimeToDelivery + x.TimeToOrder) * (-1))).Sum(s => s.NumberOfSoldProducts)) * 2 / (x.IsPallet ? x.ProductTransportPackageNumberOfPieces * x.BoxesPerPallet : x.ProductTransportPackageNumberOfPieces),
                    Package = x.IsPallet ? "Pallet" : "Box",
                }).OrderBy(x => x.Supplier)
            .ThenBy(x => x.Product)
            .Where(x => x.ProductsAvailable < x.SalesBasedOnDeliveryPeriod)
                .ToList();

                productsList.AddRange(products);
            }
            return productsList;
        }

        public IEnumerable<SelectListItem> SuppliersDropDown()
        {
            return this.dbContext.DeliveryNeededProducts.Select(p => new SelectListItem
            {
                Text = p.Supplier,
                Value = p.Supplier,

            }).ToList();
        }
    }
}
