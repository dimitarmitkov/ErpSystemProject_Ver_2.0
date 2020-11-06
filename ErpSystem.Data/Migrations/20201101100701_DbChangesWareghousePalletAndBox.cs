using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class DbChangesWareghousePalletAndBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseBoxes_Warehouses_WarehouseId",
                table: "WarehouseBoxes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehousePallets_Warehouses_WarehouseId",
                table: "WarehousePallets");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseNumber",
                table: "Warehouses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "WarehousePallets",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseNumber",
                table: "WarehousePallets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "WarehouseBoxes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseNumber",
                table: "WarehouseBoxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseBoxes_Warehouses_WarehouseId",
                table: "WarehouseBoxes",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehousePallets_Warehouses_WarehouseId",
                table: "WarehousePallets",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseBoxes_Warehouses_WarehouseId",
                table: "WarehouseBoxes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehousePallets_Warehouses_WarehouseId",
                table: "WarehousePallets");

            migrationBuilder.DropColumn(
                name: "WarehouseNumber",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "WarehouseNumber",
                table: "WarehousePallets");

            migrationBuilder.DropColumn(
                name: "WarehouseNumber",
                table: "WarehouseBoxes");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "WarehousePallets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "WarehouseBoxes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseBoxes_Warehouses_WarehouseId",
                table: "WarehouseBoxes",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehousePallets_Warehouses_WarehouseId",
                table: "WarehousePallets",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
