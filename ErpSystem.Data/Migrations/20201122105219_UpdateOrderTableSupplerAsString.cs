using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class UpdateOrderTableSupplerAsString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "Orders");
        }
    }
}
