using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Alter_Vehicles_Table_Add_IsDeleted_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsingFrom",
                table: "Vehicles");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vehicles",
                type: "bit",
                maxLength: 10,
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "UsingFrom",
                table: "Vehicles",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }
    }
}
