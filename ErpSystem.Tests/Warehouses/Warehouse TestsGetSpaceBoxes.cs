using System;
using ErpSystem.Data;
using ErpSystem.Services.Services;
using ErpSystem.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace ErpSystem.Tests.Warehouses
{
    public class Warehouse_TestsGetSpaceBoxes
    {
        [Fact]
        public void CheckCorrectGetSupplierName()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpSystemDbContext>().UseInMemoryDatabase("testFinalizeOrder");
            var dbContext = new ErpSystemDbContext(optionsBuilder.Options);
            var warehouse = new WarehouseSpace(dbContext);

            var warehouseSpaceInsert = new Warehouse
            {
                WarehouseName = "Warehouse1",
                CurrentBoxesFrontSpaceFree = 30,
                CurrentPalletsSpaceFree = 70,
            };

            dbContext.Warehouses.Add(warehouseSpaceInsert);
            dbContext.SaveChanges();

            var boxes = warehouse.GetSpaceBoxes();
            var boxSpace = 0;

            foreach (var box in boxes)
            {
                boxSpace = box.Value;
            }
            Assert.Equal(30m, boxSpace, 0);

            var pallets = warehouse.GetSpacePallets();
            var palletSpace = 0;

            foreach (var pallet in pallets)
            {
                palletSpace = pallet.Value;
            }
            Assert.Equal(70m, palletSpace, 0);

            var boxesTaken = new WarehouseBoxSpace
            {
                WarehouseNumber = 1,
                BoxSpace = 100,
            };

            var palletsTaken = new WarehousePalletSpace
            {
                WarehouseNumber = 1,
                PalletSpace = 100,
            };

            dbContext.WarehouseBoxes.Add(boxesTaken);
            dbContext.WarehousePallets.Add(palletsTaken);
            dbContext.SaveChanges();

            var boxesLeft = warehouse.GetSpaceBoxes();
            var boxLeft = new List<int>();

            foreach (var box in boxesLeft)
            {
                boxLeft.Add(box.Value);
            }

            var palletsLeft = warehouse.GetSpaceBoxes();
            var palletLeft = new List<int>();

            foreach (var pallet in palletsLeft)
            {
                palletLeft.Add(pallet.Value);
            }

            Assert.Equal(70m, boxLeft[0], 0);
            Assert.Equal(30m, boxLeft[1], 0);
            Assert.Equal(30m, palletLeft[1], 0);
            Assert.Equal(70m, palletLeft[0], 0);
        }
    }
}
