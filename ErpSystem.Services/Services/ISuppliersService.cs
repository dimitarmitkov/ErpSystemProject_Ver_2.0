using System;
using ErpSystem.Services.ViewModels.Supplier;

namespace ErpSystem.Services.Services
{
    public interface ISuppliersService
    {
        void AddSupplier(AddSupplierViewModel addSupplier);
    }
}
