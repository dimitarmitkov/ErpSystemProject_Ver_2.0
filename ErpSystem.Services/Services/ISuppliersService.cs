using System;
using System.Threading.Tasks;
using ErpSystem.Services.ViewModels.Supplier;

namespace ErpSystem.Services.Services
{
    public interface ISuppliersService
    {
        Task AddSupplier(AddSupplierViewModel addSupplier);
    }
}
