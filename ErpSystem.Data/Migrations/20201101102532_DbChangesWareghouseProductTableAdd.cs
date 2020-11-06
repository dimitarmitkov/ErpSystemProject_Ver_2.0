using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class DbChangesWareghouseProductTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Warehouses_WarehouseId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WarehouseId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BoxesDepthSpace",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "BoxesFrontSpace",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "ItemQuantityAvailable",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "PalletsSpace",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "WarehouseNumber",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ShelfDepth",
                table: "WarehouseBoxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WarehouseProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    ProductsAvailable = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseProducts", x => new { x.WarehouseId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WarehouseProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseProducts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_ProductId",
                table: "WarehouseProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "ShelfDepth",
                table: "WarehouseBoxes");

            migrationBuilder.AddColumn<int>(
                name: "BoxesDepthSpace",
                table: "Warehouses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BoxesFrontSpace",
                table: "Warehouses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemQuantityAvailable",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PalletsSpace",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseNumber",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_WarehouseId",
                table: "Products",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Warehouses_WarehouseId",
                table: "Products",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
