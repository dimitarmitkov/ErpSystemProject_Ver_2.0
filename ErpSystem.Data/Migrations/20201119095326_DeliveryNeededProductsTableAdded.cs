using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class DeliveryNeededProductsTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryNeededProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    Product = table.Column<string>(nullable: true),
                    Supplier = table.Column<string>(nullable: true),
                    OrderDays = table.Column<int>(nullable: false),
                    DeliveryDays = table.Column<int>(nullable: false),
                    TotalDeliveryTime = table.Column<int>(nullable: false),
                    ProductsAvailable = table.Column<int>(nullable: false),
                    SalesBasedOnDeliveryPeriod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryNeededProducts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryNeededProducts");
        }
    }
}
