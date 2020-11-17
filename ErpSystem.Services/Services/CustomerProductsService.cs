using System;
using System.Linq;
using ErpSystem.Data;
using ErpSystem.Models;

namespace ErpSystem.Services.Services
{
    public class CustomerProductsService : ICustomerProductsService
    {
        private readonly ErpSystemDbContext dbContext;

        public CustomerProductsService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void GenerateCustomerProduct(int productId, string customerId, int productByExpireDateId, int warehouseId, int numberOfProducts, bool hasDicount)
        {
            var currentSale = this.dbContext.CustomerProducts.Where(cp => cp.CustomerId == customerId).Select(x => x.Id).FirstOrDefault();

            var products = new CustomerProduct
            {
                ProductId = productId,
                CustomerId = customerId,
                CurrentsaleId = currentSale,
                WarehouseId = warehouseId,
                ProductsToSale = numberOfProducts,
                HasProductDiscount = hasDicount,
                WarehouseProductIdByExpireDate = productByExpireDateId,
            };

            this.dbContext.AddAsync(products);
            this.dbContext.SaveChangesAsync();
        }
    }
}
