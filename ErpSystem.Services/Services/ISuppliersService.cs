namespace ErpSystem.Services.Services
{
    using System.Threading.Tasks;

    using ErpSystem.Services.ViewModels.Supplier;

    public interface ISuppliersService
    {
        Task AddSupplier(AddSupplierViewModel addSupplier);
    }
}
