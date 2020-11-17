using System;
using System.Linq;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.Warehouse;

namespace ErpSystem.Services.Services
{
    public class WarehouseProducService : IWarehouseProductService
    {
        private readonly ErpSystemDbContext dbContext;

        public WarehouseProducService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public WarehouseProduct GetById(int Id)
        {
            return this.dbContext.WarehouseProducts.Where(w => w.Id == Id).FirstOrDefault();
        }
    }
}
