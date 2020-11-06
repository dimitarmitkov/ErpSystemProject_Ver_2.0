using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyTypeTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyTypeOfRegistration = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypeTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductMeasurmentTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maesurment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMeasurmentTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(nullable: false),
                    SupplierCountry = table.Column<string>(nullable: true),
                    SupplierPostalCode = table.Column<string>(nullable: true),
                    SupplierAddress = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    CustomsAuthorisationNeeded = table.Column<bool>(nullable: false),
                    SupplierAdditionalInformation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportPackageTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfPackage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportPackageTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseName = table.Column<string>(nullable: true),
                    ItemQuantityAvailable = table.Column<int>(nullable: false),
                    PalletsSpace = table.Column<int>(nullable: false),
                    CurrentPalletsSpaceFree = table.Column<int>(nullable: false),
                    BoxesFrontSpace = table.Column<int>(nullable: true),
                    BoxesDepthSpace = table.Column<int>(nullable: true),
                    CurrentBoxesFrontSpaceFree = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    CompanyTypeId = table.Column<int>(nullable: false),
                    CompanyTypeOfRegistrationId = table.Column<int>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    PostalCode = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    CustomerType = table.Column<int>(nullable: true),
                    CustomerRating = table.Column<int>(nullable: true),
                    CustomerDiscount = table.Column<int>(nullable: true),
                    HasDelivery = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CustomerAdditionalInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CompanyTypeTags_CompanyTypeOfRegistrationId",
                        column: x => x.CompanyTypeOfRegistrationId,
                        principalTable: "CompanyTypeTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    TransportCost = table.Column<decimal>(nullable: false),
                    CompanyExpenses = table.Column<decimal>(nullable: true),
                    OtherExpenses = table.Column<decimal>(nullable: true),
                    NumberOfTransportPackageUnits = table.Column<int>(nullable: false),
                    TotalAmountOfProductNumberOrWeight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(nullable: true),
                    CalculatedOrderProductNumber = table.Column<int>(nullable: false),
                    ReserveNumberOfOrderedProduct = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    NumberOfTransportPackageUnits = table.Column<int>(nullable: false),
                    TotalAmountOfProductNumberOrWeight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductIndentificationNumber = table.Column<string>(nullable: false),
                    ProductName = table.Column<string>(nullable: false),
                    ProductPrice = table.Column<decimal>(nullable: false),
                    ProductDiscount = table.Column<int>(nullable: true),
                    ProductGrossMargin = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: true),
                    TimeToOrder = table.Column<int>(nullable: false),
                    TimeToDelivery = table.Column<int>(nullable: false),
                    ProductionDate = table.Column<DateTime>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    ProductTransportPackageId = table.Column<int>(nullable: false),
                    MeasurementId = table.Column<int>(nullable: false),
                    MeasurmentTagId = table.Column<int>(nullable: true),
                    IsPallet = table.Column<bool>(nullable: false),
                    ProductTransportPackageWidthSize = table.Column<int>(nullable: false),
                    ProductTransportPackageLengthSize = table.Column<int>(nullable: false),
                    ProductTransportPackageHeightSize = table.Column<int>(nullable: false),
                    ProductTransportPackageWeight = table.Column<int>(nullable: false),
                    ProductTransportPackageNumberOfPieces = table.Column<int>(nullable: false),
                    BoxesPerPallet = table.Column<int>(nullable: false),
                    SingleProductSize = table.Column<string>(nullable: true),
                    ProductDescription = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductMeasurmentTags_MeasurmentTagId",
                        column: x => x.MeasurmentTagId,
                        principalTable: "ProductMeasurmentTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_TransportPackageTags_ProductTransportPackageId",
                        column: x => x.ProductTransportPackageId,
                        principalTable: "TransportPackageTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<int>(nullable: false),
                    BoxSpace = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseBoxes_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehousePallets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<int>(nullable: false),
                    PalletSpace = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehousePallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehousePallets_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSuppliers",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSuppliers", x => new { x.SupplierId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductsSuppliers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsSuppliers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(nullable: false),
                    NumberOfSoldProducts = table.Column<int>(nullable: false),
                    SaleDate = table.Column<DateTime>(nullable: false),
                    SingleProudctSalePrice = table.Column<decimal>(nullable: false),
                    HasProductDiscount = table.Column<bool>(nullable: false),
                    HasCustomerDiscount = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => new { x.CustomerId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CompanyTypeOfRegistrationId",
                table: "Customers",
                column: "CompanyTypeOfRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_SupplierId",
                table: "Deliveries",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SupplierId",
                table: "Orders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MeasurmentTagId",
                table: "Products",
                column: "MeasurmentTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTransportPackageId",
                table: "Products",
                column: "ProductTransportPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_WarehouseId",
                table: "Products",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSuppliers_ProductId",
                table: "ProductsSuppliers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseBoxes_WarehouseId",
                table: "WarehouseBoxes",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehousePallets_WarehouseId",
                table: "WarehousePallets",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductsSuppliers");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "WarehouseBoxes");

            migrationBuilder.DropTable(
                name: "WarehousePallets");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "CompanyTypeTags");

            migrationBuilder.DropTable(
                name: "ProductMeasurmentTags");

            migrationBuilder.DropTable(
                name: "TransportPackageTags");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
