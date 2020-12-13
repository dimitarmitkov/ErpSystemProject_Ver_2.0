using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpSystem.Data;
using ErpSystem.Models;
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

        // test completed
        public void GenetareOrder(CalculateNeedOfOrderViewModel calculateNeedOfOrder)
        {
            var supplierId = this.dbContext.Suppliers.Where(x => x.SupplierName == calculateNeedOfOrder.Supplier).Select(x => x.Id).FirstOrDefault();

            var order = new Order
            {
                SupplierId = supplierId,
                Supplier = calculateNeedOfOrder.Supplier,
                ProductId = calculateNeedOfOrder.ProductId,
                ProductName = calculateNeedOfOrder.Product,
                OrderDate = DateTime.UtcNow.Date,
                NumberOfTransportPackageUnitsOrdered = calculateNeedOfOrder.OrderedTarnsportUnits,
                TotalAmountOfOrder = calculateNeedOfOrder.ProductExwPrice * calculateNeedOfOrder.OrderedTarnsportUnits,
                TotalOrderWeight = calculateNeedOfOrder.TotalWeightOfTransportUnit * calculateNeedOfOrder.OrderedTarnsportUnits,
            };

            this.dbContext.Orders.Add(order);
            this.dbContext.SaveChanges();

            if (!ProductsForOrderList().Any())
            {
                var range = this.dbContext.DeliveryNeededProducts.ToList();

                for (int i = 0; i < range.Count; i++)
                {
                    this.dbContext.DeliveryNeededProducts.Remove(range[i]);
                    this.dbContext.SaveChanges();
                }
            }
        }

        // test completed
        public IEnumerable<CalculateNeedOfOrderViewModel> ProductsForOrderList()
        {
            var productOrderedId = this.dbContext.Orders.Select(x => x.ProductId).ToList();
            var supplier = this.dbContext.DeliveryNeededProducts.Select(s => s.Supplier).Distinct().ToList();
            var productsList = new List<CalculateNeedOfOrderViewModel>();

            for (int i = 0; i < supplier.Count; i++)
            {
                var products = this.dbContext.Products.Where(s => s.Supplier.SupplierName == supplier[i] && s.IsDeleted == false).Select(x => new CalculateNeedOfOrderViewModel
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
                    NumberOfTransportUnitsClaculatedForOrder = (int)Math.Ceiling(((double)this.dbContext.Sales.Where(p => p.ProductId == x.Id && p.SaleDate >= DateTime.UtcNow.AddDays((x.TimeToDelivery + x.TimeToOrder) * (-1))).Sum(s => s.NumberOfSoldProducts)) / (x.IsPallet ? x.ProductTransportPackageNumberOfPieces * x.BoxesPerPallet : x.ProductTransportPackageNumberOfPieces)),
                    Package = x.IsPallet ? "Pallet" : "Box",
                }).OrderBy(x => x.Supplier)
            .ThenBy(x => x.Product)
            .Where(x => x.ProductsAvailable < x.SalesBasedOnDeliveryPeriod && !productOrderedId.Any(a => a == x.ProductId))
                .ToList();
                productsList.AddRange(products);
            }
            return productsList;
        }

        // test completed
        public IEnumerable<int> OrdesAny()
        {
            return this.dbContext.Orders.Select(x => x.ProductId).ToList();
        }

        // test completed
        public void SelectSupplier(string supplierName)
        {
            var supplierId = this.dbContext.Suppliers.Where(s => s.SupplierName == supplierName).Select(x => x.Id).FirstOrDefault();

            var supplier = new SupplierForOrder
            {
                SupplierId = supplierId,
                SupplierName = supplierName,
            };

            var excistingRecords = this.dbContext.SupplierForOrders.Where(x => x.Id > -1).ToList();

            if (excistingRecords.Count > 0)
            {
                for (int i = 0; i < excistingRecords.Count; i++)
                {
                    this.dbContext.SupplierForOrders.Remove(excistingRecords[i]);
                }
            }
            this.dbContext.SupplierForOrders.Add(supplier);
            this.dbContext.SaveChanges();
        }

        // test completed
        public string GetSupplierName()
        {
            return this.dbContext.SupplierForOrders.Select(s => s.SupplierName).FirstOrDefault();
        }

        public IEnumerable<SelectListItem> SuppliersDropDown()
        {
            return this.dbContext.DeliveryNeededProducts.Select(p => new SelectListItem
            {
                Text = p.Supplier,
                Value = p.Supplier,
            }).ToList();
        }

        // test completed
        public async Task FinalizeOrder()
        {
            var supplierName = this.dbContext.SupplierForOrders.Select(s => s.SupplierName).FirstOrDefault();

            if (!string.IsNullOrEmpty(supplierName))
            {
                var supplierNeedForOrder = this.dbContext.SupplierForOrders.FirstOrDefault(s => s.SupplierName == supplierName);
                this.dbContext.SupplierForOrders.Remove(supplierNeedForOrder);

                var supplierDeliveryNeedProducts = this.dbContext.DeliveryNeededProducts.FirstOrDefault(s => s.Supplier == supplierName);
                this.dbContext.DeliveryNeededProducts.Remove(supplierDeliveryNeedProducts);
            }
            await this.dbContext.SaveChangesAsync();
        }
    }
}