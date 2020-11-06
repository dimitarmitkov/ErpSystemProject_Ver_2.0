using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class DbChangesSalesAccumulatorAddSoldPack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleAccumulator_Products_ProductId1",
                table: "SaleAccumulator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleAccumulator",
                table: "SaleAccumulator");

            migrationBuilder.RenameTable(
                name: "SaleAccumulator",
                newName: "SaleAccumulators");

            migrationBuilder.RenameIndex(
                name: "IX_SaleAccumulator_ProductId1",
                table: "SaleAccumulators",
                newName: "IX_SaleAccumulators_ProductId1");

            migrationBuilder.AddColumn<int>(
                name: "SoldPackagesCounter",
                table: "SaleAccumulators",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleAccumulators",
                table: "SaleAccumulators",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleAccumulators_Products_ProductId1",
                table: "SaleAccumulators",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleAccumulators_Products_ProductId1",
                table: "SaleAccumulators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleAccumulators",
                table: "SaleAccumulators");

            migrationBuilder.DropColumn(
                name: "SoldPackagesCounter",
                table: "SaleAccumulators");

            migrationBuilder.RenameTable(
                name: "SaleAccumulators",
                newName: "SaleAccumulator");

            migrationBuilder.RenameIndex(
                name: "IX_SaleAccumulators_ProductId1",
                table: "SaleAccumulator",
                newName: "IX_SaleAccumulator_ProductId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleAccumulator",
                table: "SaleAccumulator",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleAccumulator_Products_ProductId1",
                table: "SaleAccumulator",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
