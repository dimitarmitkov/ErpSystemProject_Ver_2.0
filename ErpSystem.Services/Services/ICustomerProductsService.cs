using System;
namespace ErpSystem.Services.Services
{
    public interface ICustomerProductsService
    {
        void GenerateCustomerProduct(int productId, string customerId, int productByExpireDateId, int warehouseId, int numberOfProducts, bool hasDicount);
    }
}
