using System;
using System.Linq;
using ErpSystem.Data;
using ErpSystem.Services.ViewModels.Order;

namespace ErpSystem.Services.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ErpSystemDbContext dbContext;

        public OrdersService(ErpSystemDbContext dbContext, ISalesService salesService)
        {
            this.dbContext = dbContext;
            SalesService = salesService;
        }

        public ISalesService SalesService { get; }

        public void GenetareOrder(GenerateOrderViewModel generateOrder)
        {
            var order = new GenerateOrderViewModel
            {
                Supplier = generateOrder.Supplier,
                OrderDate = DateTime.UtcNow,
                ProductId = generateOrder.ProductId,
            };

            bool isPallet = this.dbContext.Products.Where(p => p.Id == generateOrder.ProductId).Select(x => x.IsPallet).FirstOrDefault();
            var piecesPerBox = this.dbContext.Products.Where(p => p.Id == generateOrder.ProductId).Select(x => x.ProductTransportPackageNumberOfPieces).FirstOrDefault();
            var boxesPerPallet = this.dbContext.Products.Where(p => p.Id == generateOrder.ProductId).Select(x => x.BoxesPerPallet).FirstOrDefault();
            var salesQuantity = this.SalesService.ListOfSales().Where(x => x.ProductId == generateOrder.ProductId).Sum(x => x.NumberOfSoldProducts);
            var productTransportPackage = isPallet ? piecesPerBox * boxesPerPallet : piecesPerBox;

            order.CalculatedOrderProductNumber = (int)Math.Ceiling((double)salesQuantity / productTransportPackage);
        }
    }
}
