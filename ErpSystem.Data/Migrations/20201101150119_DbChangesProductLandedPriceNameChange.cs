using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class DbChangesProductLandedPriceNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductLendedPrice",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductLandedPrice",
                table: "Products",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductLandedPrice",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductLendedPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
