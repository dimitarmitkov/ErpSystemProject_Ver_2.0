namespace ErpSystem.Services.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using ErpSystem.Data;
    using ErpSystem.Models;
    using ErpSystem.Services.ViewModels.Supplier;

    public class SuppliersService : ISuppliersService
    {
        private readonly ErpSystemDbContext dbContext;
        private readonly IMapper mapper;

        public SuppliersService(ErpSystemDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // create supplier, test done
        public async Task AddSupplier(AddSupplierViewModel addSupplier)
        {
            var supplier = this.mapper.Map<Supplier>(addSupplier);

            await this.dbContext.Suppliers.AddAsync(supplier);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
