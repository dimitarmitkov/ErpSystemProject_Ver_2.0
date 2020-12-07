using System.Collections.Generic;
using System.Linq;
using ErpSystem.Data;

namespace ErpSystem.Services.Services
{
    public class WarehouseSpace : IWarehouseSpace
    {
        private readonly ErpSystemDbContext dbContext;

        public WarehouseSpace(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<KeyValuePair<string, int>> GetSpaceBoxes()
        {
            var currentBoxesSpaceTaken = this.dbContext.Warehouses.Select(b => b.CurrentBoxesFrontSpaceFree).FirstOrDefault();
            var currentBoxesSpaceFree = this.dbContext.WarehouseBoxes.Select(b => b.BoxSpace).FirstOrDefault() - currentBoxesSpaceTaken;

            var dictionary = new Dictionary<string, int>();

            dictionary["boxesTaken"] = currentBoxesSpaceTaken;
            dictionary["boxesFree"] = currentBoxesSpaceFree;

            return dictionary;
        }

        public IEnumerable<KeyValuePair<string, int>> GetSpacePallets()
        {
            var currentPalletsSpaceTaken = this.dbContext.Warehouses.Select(p => p.CurrentPalletsSpaceFree).FirstOrDefault();
            var currenPalletsSpaceFree = this.dbContext.WarehousePallets.Select(p => p.PalletSpace).FirstOrDefault() - currentPalletsSpaceTaken;

            var dictionary = new Dictionary<string, int>();

            dictionary["palletsTaken"] = currentPalletsSpaceTaken;
            dictionary["palletsFree"] = currenPalletsSpaceFree;

            return dictionary;
        }
    }
}
