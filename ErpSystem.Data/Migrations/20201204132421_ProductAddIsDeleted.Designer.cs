﻿// <auto-generated />
using System;
using ErpSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ErpSystem.Data.Migrations
{
    [DbContext(typeof(ErpSystemDbContext))]
    [Migration("20201204132421_ProductAddIsDeleted")]
    partial class ProductAddIsDeleted
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ErpSystem.Models.CompanyTypeTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyTypeOfRegistration")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CompanyTypeTags");
                });

            modelBuilder.Entity("ErpSystem.Models.CurrentSale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerEikNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasCustomerDiscount")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CurrentSales");
                });

            modelBuilder.Entity("ErpSystem.Models.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyEik")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyTypeOfRegistrationId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerAdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerDiscount")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerRating")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerType")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasDelivery")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyTypeOfRegistrationId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ErpSystem.Models.CustomerProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentsaleId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasProductDiscount")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsToSale")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseProductIdByExpireDate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CurrentsaleId");

                    b.ToTable("CustomerProducts");
                });

            modelBuilder.Entity("ErpSystem.Models.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("CompanyExpenses")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DeliveryDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfTransportPackageUnits")
                        .HasColumnType("int");

                    b.Property<decimal?>("OtherExpenses")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmountOfProductNumberOrWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TransportCost")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("ErpSystem.Models.DeliveryNeededProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ConfimBeenNoticed")
                        .HasColumnType("bit");

                    b.Property<int>("DeliveryDays")
                        .HasColumnType("int");

                    b.Property<int>("OrderDays")
                        .HasColumnType("int");

                    b.Property<string>("Product")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsAvailable")
                        .HasColumnType("int");

                    b.Property<int>("SalesBasedOnDeliveryPeriod")
                        .HasColumnType("int");

                    b.Property<string>("Supplier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalDeliveryTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DeliveryNeededProducts");
                });

            modelBuilder.Entity("ErpSystem.Models.FinalizedOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumberOfTransportPackageUnitsOrdered")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Supplier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmountOfOrder")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("TotalOrderWeight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("FinalizedOrders");
                });

            modelBuilder.Entity("ErpSystem.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumberOfTransportPackageUnitsOrdered")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Supplier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmountOfOrder")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("TotalOrderWeight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ErpSystem.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BoxesPerPallet")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPallet")
                        .HasColumnType("bit");

                    b.Property<int>("MeasurementId")
                        .HasColumnType("int");

                    b.Property<int?>("MeasurmentTagId")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductDiscount")
                        .HasColumnType("int");

                    b.Property<int>("ProductGrossMargin")
                        .HasColumnType("int");

                    b.Property<string>("ProductIndentificationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductLandedPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductSalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductTransportPackageHeightSize")
                        .HasColumnType("int");

                    b.Property<int>("ProductTransportPackageId")
                        .HasColumnType("int");

                    b.Property<int>("ProductTransportPackageLengthSize")
                        .HasColumnType("int");

                    b.Property<int>("ProductTransportPackageNumberOfPieces")
                        .HasColumnType("int");

                    b.Property<int>("ProductTransportPackageWeight")
                        .HasColumnType("int");

                    b.Property<int>("ProductTransportPackageWidthSize")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ProductionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SingleProductSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.Property<int>("TimeToDelivery")
                        .HasColumnType("int");

                    b.Property<int>("TimeToOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeasurmentTagId");

                    b.HasIndex("ProductTransportPackageId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ErpSystem.Models.ProductMeasurmentTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Maesurment")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductMeasurmentTags");
                });

            modelBuilder.Entity("ErpSystem.Models.ProductSupplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("SupplierId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductsSuppliers");
                });

            modelBuilder.Entity("ErpSystem.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("HasCustomerDiscount")
                        .HasColumnType("bit");

                    b.Property<bool>("HasProductDiscount")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfSoldProducts")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SingleProudctSalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("ErpSystem.Models.SaleAccumulator", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId1")
                        .HasColumnType("int");

                    b.Property<int>("SoldBoxCounter")
                        .HasColumnType("int");

                    b.Property<int>("SoldPalletCounter")
                        .HasColumnType("int");

                    b.Property<int>("SoldProductsAccumulator")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductId1");

                    b.ToTable("SaleAccumulators");
                });

            modelBuilder.Entity("ErpSystem.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CustomsAuthorisationNeeded")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierAdditionalInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierPostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ErpSystem.Models.SupplierForOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<string>("SupplierName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SupplierForOrders");
                });

            modelBuilder.Entity("ErpSystem.Models.TransportPackageTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeOfPackage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransportPackageTags");
                });

            modelBuilder.Entity("ErpSystem.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ErpSystem.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentBoxesFrontSpaceFree")
                        .HasColumnType("int");

                    b.Property<int>("CurrentPalletsSpaceFree")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("WarehouseName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("ErpSystem.Models.WarehouseBoxSpace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BoxSpace")
                        .HasColumnType("int");

                    b.Property<int>("ShelfDepth")
                        .HasColumnType("int");

                    b.Property<int?>("WarehouseId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseBoxes");
                });

            modelBuilder.Entity("ErpSystem.Models.WarehousePalletSpace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PalletSpace")
                        .HasColumnType("int");

                    b.Property<int?>("WarehouseId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehousePallets");
                });

            modelBuilder.Entity("ErpSystem.Models.WarehouseProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ProductionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductsAvailable")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseProducts");
                });

            modelBuilder.Entity("ErpSystem.Models.Customer", b =>
                {
                    b.HasOne("ErpSystem.Models.CompanyTypeTag", "CompanyTypeOfRegistration")
                        .WithMany("Customers")
                        .HasForeignKey("CompanyTypeOfRegistrationId");
                });

            modelBuilder.Entity("ErpSystem.Models.CustomerProduct", b =>
                {
                    b.HasOne("ErpSystem.Models.CurrentSale", "CurrentSale")
                        .WithMany("CusomerProducts")
                        .HasForeignKey("CurrentsaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ErpSystem.Models.Delivery", b =>
                {
                    b.HasOne("ErpSystem.Models.Supplier", "Supplier")
                        .WithMany("Deliveries")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("ErpSystem.Models.Order", b =>
                {
                    b.HasOne("ErpSystem.Models.Supplier", null)
                        .WithMany("Orders")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ErpSystem.Models.Product", b =>
                {
                    b.HasOne("ErpSystem.Models.ProductMeasurmentTag", "MeasurmentTag")
                        .WithMany("Products")
                        .HasForeignKey("MeasurmentTagId");

                    b.HasOne("ErpSystem.Models.TransportPackageTag", "ProductTransportPackage")
                        .WithMany("Products")
                        .HasForeignKey("ProductTransportPackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ErpSystem.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("ErpSystem.Models.ProductSupplier", b =>
                {
                    b.HasOne("ErpSystem.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ErpSystem.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ErpSystem.Models.Sale", b =>
                {
                    b.HasOne("ErpSystem.Models.Customer", "Customer")
                        .WithMany("Purchase")
                        .HasForeignKey("CustomerId");

                    b.HasOne("ErpSystem.Models.Product", "Product")
                        .WithMany("Sales")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ErpSystem.Models.SaleAccumulator", b =>
                {
                    b.HasOne("ErpSystem.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ErpSystem.Models.Warehouse", b =>
                {
                    b.HasOne("ErpSystem.Models.Product", null)
                        .WithMany("Warehouses")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("ErpSystem.Models.WarehouseBoxSpace", b =>
                {
                    b.HasOne("ErpSystem.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId");
                });

            modelBuilder.Entity("ErpSystem.Models.WarehousePalletSpace", b =>
                {
                    b.HasOne("ErpSystem.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId");
                });

            modelBuilder.Entity("ErpSystem.Models.WarehouseProduct", b =>
                {
                    b.HasOne("ErpSystem.Models.Product", "Product")
                        .WithOne("WarehouseProduct")
                        .HasForeignKey("ErpSystem.Models.WarehouseProduct", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ErpSystem.Models.Warehouse", "Warehouse")
                        .WithMany("WarehouseProducts")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
