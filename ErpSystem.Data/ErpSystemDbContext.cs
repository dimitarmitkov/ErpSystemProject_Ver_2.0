using System;
using ErpSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Data
{
    public class ErpSystemDbContext : DbContext
    {
        public ErpSystemDbContext()
        {

        }

        public ErpSystemDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CompanyTypeTag> CompanyTypeTags { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<ProductMeasurmentTag> ProductMeasurmentTags { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<TransportPackageTag> TransportPackageTags { get; set; }
        public DbSet<ProductSupplier> ProductsSuppliers { get; set; }
        public DbSet<WarehousePalletSpace> WarehousePallets { get; set; }
        public DbSet<WarehouseBoxSpace> WarehouseBoxes { get; set; }
        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
        public DbSet<SaleAccumulator> SaleAccumulators { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CurrentSale> CurrentSales { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }
        public DbSet<DeliveryNeededProduct> DeliveryNeededProducts { get; set; }
        public DbSet<SupplierForOrder> SupplierForOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = localhost, 1433; Database = ERP; User = sa; Password = reallyStrongPwd123; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<ProductSupplier>()
                .HasKey(x => new { x.SupplierId, x.ProductId });

            //modelBuilder.Entity<WarehouseProduct>()
            //    .HasKey(x => new { x.WarehouseId, x.ProductId });

            modelBuilder.Entity<SaleAccumulator>()
                .HasKey(x => x.ProductId);
        }
    }
}
