using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.Delivery;

namespace ErpSystem.Services.Services
{
    public class DeliveriesService : IDeliveriesService
    {
        private const int ProductsPerPage = 2;
        private readonly ErpSystemDbContext dbContext;

        public DeliveriesService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // test completed
        public IEnumerable<DeliveryListViewModel> GetAllOrdersForDelivery(int page, int itemsPerPage = ProductsPerPage)
        {
            var suppliersList = this.dbContext.Orders.Select(x => x.SupplierId).Distinct().ToList();

            var listForDelivery = new List<DeliveryListViewModel>();

            for (int i = 0; i < suppliersList.Count(); i++)
            {
                var list = this.dbContext.Orders.Where(s => s.SupplierId == suppliersList[i]).Select(x => new DeliveryListViewModel
                {
                    Supplier = x.Supplier,
                    ProductId = x.ProductId,
                    Product = x.ProductName,
                    NumberOfTransportUnits = this.dbContext.Products.Where(p => p.Id == x.ProductId).Select(y => y.ProductTransportPackageNumberOfPieces).FirstOrDefault(),
                    TotalProductPrice = x.TotalAmountOfOrder,
                    TotalWeightOfTransportUnit = this.dbContext.Products.Where(p => p.Id == x.ProductId && p.IsDeleted == false).Select(y => y.ProductTransportPackageWeight).FirstOrDefault(),
                    ProductMeasurementType = this.dbContext.Products.Where(p => p.Id == x.ProductId && p.IsDeleted == false).Select(y => y.MeasurmentTag.Maesurment).FirstOrDefault(),
                    OrderDate = x.OrderDate,
                    Package = this.dbContext.Products.Where(p => p.Id == x.ProductId).Select(y => y.ProductTransportPackage.TypeOfPackage).FirstOrDefault(),
                    TotalOrderPrice = x.TotalAmountOfOrder * this.dbContext.Products.Where(p => p.Id == x.ProductId).Select(y => y.ProductTransportPackageNumberOfPieces).FirstOrDefault(),
                    TotalOrderWeight = this.dbContext.Orders.Where(s => s.SupplierId == x.SupplierId).Sum(x => x.TotalOrderWeight),

                }).ToList();

                listForDelivery.AddRange(list);
            }
            return listForDelivery.OrderBy(x => x.Supplier).ThenBy(x => x.Product).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
        }

        // test completed
        public void FinalizeDelivery(DeliveryListViewModel deliveryList)
        {
            var productOrdered = this.dbContext.Orders.Where(x => x.ProductId == deliveryList.ProductId).FirstOrDefault();

            this.dbContext.Orders.Remove(productOrdered);
            this.dbContext.SaveChanges();

            var addPorductInWarehouseProducts = new WarehouseProduct
            {
                Product = this.dbContext.Products.Where(p => p.Id == deliveryList.ProductId && p.IsDeleted == false).FirstOrDefault(),
                ProductId = deliveryList.ProductId,
                ProductionDate = deliveryList.ProductionDate,
                ExpireDate = deliveryList.ExpireDate,
                Warehouse = this.dbContext.WarehouseProducts.Where(p => p.ProductId == deliveryList.ProductId).Select(x => x.Warehouse).FirstOrDefault(),
                WarehouseId = this.dbContext.WarehouseProducts.Where(p => p.ProductId == deliveryList.ProductId).Select(x => x.WarehouseId).FirstOrDefault(),
            };

            bool isProductInPallet = this.dbContext.Products.Where(p => p.Id == deliveryList.ProductId && p.IsDeleted == false).Select(p => p.IsPallet == true).FirstOrDefault();
            var numberOfProductsPerBox = this.dbContext.Products.Where(p => p.Id == deliveryList.ProductId && p.IsDeleted == false).Select(x => x.ProductTransportPackageNumberOfPieces).FirstOrDefault();
            var boxesPerPallet = this.dbContext.Products.Where(p => p.Id == deliveryList.ProductId && p.IsDeleted == false).Select(x => x.BoxesPerPallet).FirstOrDefault();
            var numberOfProductPerPallet = numberOfProductsPerBox * boxesPerPallet;
            var warehousePalletsSpace = addPorductInWarehouseProducts.Warehouse.CurrentPalletsSpaceFree;
            var warehouseBoxessSpace = addPorductInWarehouseProducts.Warehouse.CurrentBoxesFrontSpaceFree;

            deliveryList.ChangedNumberOfTransportUnits = deliveryList.ChangedNumberOfTransportUnits == 0 ? productOrdered.NumberOfTransportPackageUnitsOrdered : deliveryList.ChangedNumberOfTransportUnits;

            addPorductInWarehouseProducts.ProductsAvailable += isProductInPallet ? deliveryList.ChangedNumberOfTransportUnits * numberOfProductPerPallet : deliveryList.ChangedNumberOfTransportUnits * numberOfProductsPerBox;

            this.dbContext.WarehouseProducts.Add(addPorductInWarehouseProducts);
            this.dbContext.SaveChanges();

            if (isProductInPallet)
            {
                addPorductInWarehouseProducts.Warehouse.CurrentPalletsSpaceFree -= deliveryList.NumberOfTransportUnits;
                this.dbContext.Warehouses.Update(addPorductInWarehouseProducts.Warehouse);
                this.dbContext.SaveChanges();
            }

            if (!isProductInPallet)
            {
                // get box size and shelf width size
                var boxFront = this.dbContext.Products.Where(p => p.Id == deliveryList.ProductId && p.IsDeleted == false).Select(x => x.ProductTransportPackageWidthSize).FirstOrDefault();
                var boxLenght = this.dbContext.Products.Where(p => p.Id == deliveryList.ProductId && p.IsDeleted == false).Select(x => x.ProductTransportPackageLengthSize).FirstOrDefault();
                var shelfDepth = this.dbContext.WarehouseBoxes.Where(w => w.Id == deliveryList.ProductId).Select(x => x.ShelfDepth).FirstOrDefault();


                addPorductInWarehouseProducts.Warehouse.CurrentBoxesFrontSpaceFree -= boxLenght > shelfDepth ? (deliveryList.NumberOfTransportUnits) * boxLenght : (deliveryList.NumberOfTransportUnits) * boxFront;

                this.dbContext.Warehouses.Update(addPorductInWarehouseProducts.Warehouse);
                this.dbContext.SaveChanges();
            }

            var productFinalizedOrder = new FinalizedOrder
            {
                ProductId = productOrdered.ProductId,
                ProductName = productOrdered.ProductName,
                Supplier = productOrdered.Supplier,
                SupplierId = productOrdered.SupplierId,
                NumberOfTransportPackageUnitsOrdered = productOrdered.NumberOfTransportPackageUnitsOrdered,
                OrderDate = productOrdered.OrderDate,
                TotalAmountOfOrder = productOrdered.TotalAmountOfOrder,
                TotalOrderWeight = productOrdered.TotalOrderWeight,
            };

            this.dbContext.FinalizedOrders.Add(productFinalizedOrder);
            this.dbContext.SaveChanges();
        }

        // get cout, test completed 
        public int GetCount()
        {
            return this.dbContext.Orders.Count();
        }
    }
}