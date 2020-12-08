using AutoMapper;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.Supplier;
using Microsoft.Extensions.DependencyInjection;

namespace ErpSystem.Services.Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly ErpSystemDbContext dbContext;
        private readonly IMapper mapper;

        public SuppliersService(ErpSystemDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<AddSupplierViewModel, Supplier>();
                mc.CreateMap<Supplier, AddSupplierViewModel>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void AddSupplier(AddSupplierViewModel addSupplier)
        {

            var supplier = mapper.Map<Supplier>(addSupplier);

            this.dbContext.Suppliers.Add(supplier);
            this.dbContext.SaveChanges();
        }
    }
}
