using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class DataBaseChangesProdAndExpDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseProducts",
                table: "WarehouseProducts");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WarehouseProducts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "WarehouseProducts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionDate",
                table: "WarehouseProducts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseProducts",
                table: "WarehouseProducts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProducts_WarehouseId",
                table: "WarehouseProducts",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseProducts",
                table: "WarehouseProducts");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseProducts_WarehouseId",
                table: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "WarehouseProducts");

            migrationBuilder.DropColumn(
                name: "ProductionDate",
                table: "WarehouseProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseProducts",
                table: "WarehouseProducts",
                columns: new[] { "WarehouseId", "ProductId" });
        }
    }
}
