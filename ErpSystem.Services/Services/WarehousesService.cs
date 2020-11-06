using System;
using System.Linq;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.Warehouse;

namespace ErpSystem.Services.Services
{

    public class WarehousesService : IWarehousesService
    {
        private readonly ErpSystemDbContext dbContext;

        public WarehousesService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public const int warehouseInitialPalletSpace = 200;
        public const int warehouseInitialBoxesSpace = 750;

        public void AddProduct(AddProductWaerhouseViewModel addProduct)
        {

            var palletSpace = this.dbContext.Warehouses.Select(w => w.CurrentPalletsSpaceFree).FirstOrDefault();

            var boxSpace = this.dbContext.Warehouses.Select(w => w.CurrentBoxesFrontSpaceFree).FirstOrDefault();

            var producTransportPackage = this.dbContext.Products.Where(p => p.Id == addProduct.ProductId).Select(x => x.IsPallet).FirstOrDefault();

            int productsPerBox = this.dbContext.Products.Where(p => p.Id == addProduct.ProductId).Select(x => x.ProductTransportPackageNumberOfPieces).FirstOrDefault();

            int boxesPerPallet = this.dbContext.Products.Where(p => p.Id == addProduct.ProductId).Select(x => x.BoxesPerPallet).FirstOrDefault();

            var shelfDepth = this.dbContext.WarehouseBoxes.Select(w => w.ShelfDepth).FirstOrDefault();

            var boxesDepthSpace = this.dbContext.Products.Where(p => p.Id == addProduct.ProductId).Select(x => x.ProductTransportPackageLengthSize).FirstOrDefault();

            var boxesFrontSpace = this.dbContext.Products.Where(p => p.Id == addProduct.ProductId).Select(x => x.ProductTransportPackageWidthSize).FirstOrDefault();

            var existingProduct = this.dbContext.WarehouseProducts.Any(p => p.ProductId == addProduct.ProductId);

            if (existingProduct)
            {
                var product = this.dbContext.WarehouseProducts.FirstOrDefault(x => x.ProductId == addProduct.ProductId && x.WarehouseId == addProduct.WarehouseId);

                ProductAddQuantityPalletOrBoxSpace(addProduct, ref palletSpace, ref boxSpace, producTransportPackage, shelfDepth, boxesDepthSpace, boxesFrontSpace, product, productsPerBox, boxesPerPallet);

                this.dbContext.WarehouseProducts.Update(product);
                this.dbContext.SaveChanges();
            }
            else
            {
                var product = new WarehouseProduct
                {
                    ProductId = addProduct.ProductId,
                    WarehouseId = addProduct.WarehouseId,
                    Product = this.dbContext.Products.FirstOrDefault(p => p.Id == addProduct.ProductId),
                };

                ProductAddQuantityPalletOrBoxSpace(addProduct, ref palletSpace, ref boxSpace, producTransportPackage, shelfDepth, boxesDepthSpace, boxesFrontSpace, product, productsPerBox, boxesPerPallet);

                this.dbContext.WarehouseProducts.Add(product);
                this.dbContext.SaveChanges();
            }

        }

        private void ProductAddQuantityPalletOrBoxSpace(AddProductWaerhouseViewModel addProduct, ref int palletSpace, ref int boxSpace, bool producTransportPackage, int shelfDepth, int boxesDepthSpace, int boxesFrontSpace, WarehouseProduct product, int productsPerBox, int boxesPerPallet)
        {
            product.ProductsAvailable += addProduct.AddQuantity;
            var productWarehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.Id == addProduct.WarehouseId);



            bool isWholePallet = (addProduct.AddQuantity % (productsPerBox * boxesPerPallet)) == 0 ? true : false;

            //TODO number of pallets depending on order
            palletSpace = isWholePallet == true ? palletSpace - addProduct.SpaceTaken : palletSpace;
            productWarehouse.CurrentPalletsSpaceFree = palletSpace;

            boxSpace =
            //below check if product is not in pallet transport package, which menais it is in box:
            isWholePallet == false ?
            //below check if product box depth space > self depth:
            (boxesDepthSpace > shelfDepth) ?
            boxSpace - boxesDepthSpace * addProduct.SpaceTaken :
            boxSpace - boxesFrontSpace * addProduct.SpaceTaken :
            boxSpace;
            productWarehouse.CurrentBoxesFrontSpaceFree = boxSpace;
        }
    }
}
