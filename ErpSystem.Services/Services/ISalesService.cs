using System.Collections.Generic;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.CustomerWarehouse;
using ErpSystem.Services.ViewModels.Order;
using ErpSystem.Services.ViewModels.Sale;
using ErpSystem.Services.ViewModels.Warehouse;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.Services
{
    public interface ISalesService
    {
        void CreateSale(int productId, string customerId, int numberOfSoldProducts, bool hasProductDiscount, bool hasCustomerDiscount, int warehouseId, int productByExpireDateId);

        IEnumerable<SalesPerCustomerOrProductViewModel> ListOfSales(string customerName, string productName);

        IEnumerable<SalesPerCustomerOrProductViewModel> ListOfAllSales();

        public IEnumerable<KeyValuePair<string, decimal>> TotalSalesPerDate();

        IEnumerable<WarehouseProductViewModel> ListOfProductsForSale();

        IEnumerable<SelectListItem> SeclectCustomerDropDown();

        void GenerateCurrentSale(string companyEik, bool hasDiscount);

        IEnumerable<WarehouseProductViewModel> ListOfProductsForSaleWithCustomer();

        void IsProductForOrder(int currentId);

        IEnumerable<CalculateNeedOfOrderViewModel> AreAnyProductsForOrder();

        void ConfirmNeedOfOrder(DeliveryNeededProduct deliveryNeededProduct);
    }
}
