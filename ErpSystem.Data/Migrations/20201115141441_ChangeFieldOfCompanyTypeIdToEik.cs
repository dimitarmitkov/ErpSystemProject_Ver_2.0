using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSystem.Data.Migrations
{
    public partial class ChangeFieldOfCompanyTypeIdToEik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyTypeId",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "CompanyEik",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyEik",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CompanyTypeId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
