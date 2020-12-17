namespace ErpSystem.Services.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using ErpSystem.Data;
    using ErpSystem.Models;

    public class WarehouseProducService : IWarehouseProductService
    {
        private readonly ErpSystemDbContext dbContext;

        public WarehouseProducService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public WarehouseProduct GetById(int id)
        {
            return this.dbContext.WarehouseProducts.Where(w => w.Id == id).FirstOrDefault();
        }

        public IEnumerable<KeyValuePair<string, int>> CurrentSpace()
        {
            var currentPalletsSpaceTaken = this.dbContext.Warehouses.Select(p => p.CurrentPalletsSpaceFree).FirstOrDefault();
            var currentBoxesSpaceTaken = this.dbContext.Warehouses.Select(b => b.CurrentBoxesFrontSpaceFree).FirstOrDefault();
            var currenPalletsSpaceFree = this.dbContext.WarehousePallets.Select(p => p.PalletSpace).FirstOrDefault() - currentPalletsSpaceTaken;
            var currentBoxesSpaceFree = this.dbContext.WarehouseBoxes.Select(b => b.BoxSpace).FirstOrDefault() - currentBoxesSpaceTaken;

            var dictionary = new Dictionary<string, int>();

            dictionary["palletsTaken"] = currentPalletsSpaceTaken;
            dictionary["boxesTaken"] = currentBoxesSpaceTaken;
            dictionary["palletsFree"] = currenPalletsSpaceFree;
            dictionary["boxesFree"] = currentBoxesSpaceFree;

            return dictionary;
        }
    }
}
