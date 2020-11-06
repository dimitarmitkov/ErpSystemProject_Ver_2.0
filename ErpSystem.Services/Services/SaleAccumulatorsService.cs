using System;
using System.Linq;
using ErpSystem.Data;
using ErpSystem.Models;

namespace ErpSystem.Services.Services
{
    public class SaleAccumulatorsService : ISaleAccumulatorsService
    {
        private readonly ErpSystemDbContext dbContext;

        public SaleAccumulatorsService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CountBoxes()
        {

        }

        public void CountPallets()
        {
            throw new NotImplementedException();
        }

        public void RemoveBoxFromWarehouse()
        {
            throw new NotImplementedException();
        }

        public void RemovePalletFromWarehouse()
        {
            throw new NotImplementedException();
        }
    }
}
