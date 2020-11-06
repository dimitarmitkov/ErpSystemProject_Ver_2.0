using System;
using ErpSystem.Services.ViewModels.Warehouse;

namespace ErpSystem.Services.Services
{
    public interface IWarehousesService
    {
        void AddProduct(AddProductWaerhouseViewModel addProduct);
    }
}
