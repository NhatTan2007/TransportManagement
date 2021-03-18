using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class ALTER_VEHICLES_TABLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FuleConsumptionPerTone",
                table: "Vehicles",
                newName: "FuelConsumptionPerTone");

            migrationBuilder.AddColumn<string>(
                name: "Specifications",
                table: "Vehicles",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsingFrom",
                table: "Vehicles",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specifications",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UsingFrom",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "FuelConsumptionPerTone",
                table: "Vehicles",
                newName: "FuleConsumptionPerTone");
        }
    }
}
