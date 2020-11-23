using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class UpdateOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Suppliers_SupplierId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CalculatedOrderProductNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NumberOfTransportPackageUnits",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReserveNumberOfOrderedProduct",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalAmountOfProductNumberOrWeight",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTransportPackageUnitsOrdered",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmountOfOrder",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "TotalOrderWeight",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Suppliers_SupplierId",
                table: "Orders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Suppliers_SupplierId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NumberOfTransportPackageUnitsOrdered",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalAmountOfOrder",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalOrderWeight",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CalculatedOrderProductNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTransportPackageUnits",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReserveNumberOfOrderedProduct",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmountOfProductNumberOrWeight",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Suppliers_SupplierId",
                table: "Orders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
