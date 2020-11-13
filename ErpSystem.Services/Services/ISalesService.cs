using System.Collections.Generic;
using ErpSystem.Services.ViewModels.Sale;

namespace ErpSystem.Services.Services
{
    public interface ISalesService
    {
        void CreateSale(int productId, string customerId, int numberOfProducts, bool hasproductDiscount, bool hasCustomerDiscount, int warehouseId);

        IEnumerable<SalesPerCustomerOrProductViewModel> ListOfSales(string customerName, string productName);

        IEnumerable<SalesPerCustomerOrProductViewModel> ListOfAllSales();

        public IEnumerable<KeyValuePair<string, decimal>> TotalSalesPerDate();
    }
}
