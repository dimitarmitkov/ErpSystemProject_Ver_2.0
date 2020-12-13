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

        // test completed
        public IEnumerable<KeyValuePair<string, int>> GetSpaceBoxes()
        {
            var currentBoxesSpaceFree = this.dbContext.Warehouses.Select(b => b.CurrentBoxesFrontSpaceFree).FirstOrDefault();
            var currentBoxesSpaceTaken = this.dbContext.WarehouseBoxes.Select(b => b.BoxSpace).FirstOrDefault() - currentBoxesSpaceFree;

            var dictionary = new Dictionary<string, int>();

            dictionary["boxesTaken"] = currentBoxesSpaceTaken;
            dictionary["boxesFree"] = currentBoxesSpaceFree;

            return dictionary;
        }

        // test completed
        public IEnumerable<KeyValuePair<string, int>> GetSpacePallets()
        {
            var currenPalletsSpaceFree = this.dbContext.Warehouses.Select(p => p.CurrentPalletsSpaceFree).FirstOrDefault();
            var currentPalletsSpaceTaken = this.dbContext.WarehousePallets.Select(p => p.PalletSpace).FirstOrDefault() - currenPalletsSpaceFree;

            var dictionary = new Dictionary<string, int>();

            dictionary["palletsTaken"] = currentPalletsSpaceTaken;
            dictionary["palletsFree"] = currenPalletsSpaceFree;

            return dictionary;
        }
    }
}
