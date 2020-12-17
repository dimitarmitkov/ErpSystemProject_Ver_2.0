namespace ErpSystem.Services.Services
{
    using System;

    public interface ICustomerProductsService
    {
        void GenerateCustomerProduct(int productId, string customerId, int productByExpireDateId, int warehouseId, int numberOfProducts, bool hasDicount);
    }
}
