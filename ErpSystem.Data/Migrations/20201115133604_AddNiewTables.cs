using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class AddNiewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentSales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerEikNumber = table.Column<string>(nullable: true),
                    HasCustomerDiscount = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductsToSale = table.Column<int>(nullable: false),
                    HasProductDiscount = table.Column<bool>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    CurrentsaleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerProducts_CurrentSales_CurrentsaleId",
                        column: x => x.CurrentsaleId,
                        principalTable: "CurrentSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProducts_CurrentsaleId",
                table: "CustomerProducts",
                column: "CurrentsaleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerProducts");

            migrationBuilder.DropTable(
                name: "CurrentSales");
        }
    }
}
