using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class CurrentSalesAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Warehouses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_ProductId",
                table: "Warehouses",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Products_ProductId",
                table: "Warehouses",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Products_ProductId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_ProductId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Warehouses");
        }
    }
}
