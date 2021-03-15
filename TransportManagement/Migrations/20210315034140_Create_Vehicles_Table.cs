using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Create_Vehicles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_VehicleBrands_VehicleBrandId",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_VehicleBrandId",
                table: "Vehicles",
                newName: "IX_Vehicles_VehicleBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "LicensePlate");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleBrands_VehicleBrandId",
                table: "Vehicles",
                column: "VehicleBrandId",
                principalTable: "VehicleBrands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleBrands_VehicleBrandId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VehicleBrandId",
                table: "Vehicle",
                newName: "IX_Vehicle_VehicleBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "LicensePlate");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_VehicleBrands_VehicleBrandId",
                table: "Vehicle",
                column: "VehicleBrandId",
                principalTable: "VehicleBrands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
