namespace ErpSystem.Services.Services
{
    using System;
    using ErpSystem.Data;

    public class SaleAccumulatorsService : ISaleAccumulatorsService
    {
        private readonly ErpSystemDbContext dbContext;

        public SaleAccumulatorsService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CountBoxes()
        {
            throw new NotImplementedException();
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
