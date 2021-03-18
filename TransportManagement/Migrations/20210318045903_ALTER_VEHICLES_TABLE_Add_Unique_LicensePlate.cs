using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class ALTER_VEHICLES_TABLE_Add_Unique_LicensePlate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayJob_AspNetUsers_DriverId",
                table: "DayJob");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportInformation_DayJob_DayJobId",
                table: "TransportInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportInformation_RouteInformations_RouteId",
                table: "TransportInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportInformation_Vehicles_VehicleLicensePlate",
                table: "TransportInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransportInformation",
                table: "TransportInformation");

            migrationBuilder.DropIndex(
                name: "IX_TransportInformation_VehicleLicensePlate",
                table: "TransportInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayJob",
                table: "DayJob");

            migrationBuilder.DropColumn(
                name: "VehicleLicensePlate",
                table: "TransportInformation");

            migrationBuilder.RenameTable(
                name: "TransportInformation",
                newName: "TransportInformations");

            migrationBuilder.RenameTable(
                name: "DayJob",
                newName: "DayJobs");

            migrationBuilder.RenameIndex(
                name: "IX_TransportInformation_RouteId",
                table: "TransportInformations",
                newName: "IX_TransportInformations_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_TransportInformation_DayJobId",
                table: "TransportInformations",
                newName: "IX_TransportInformations_DayJobId");

            migrationBuilder.RenameIndex(
                name: "IX_DayJob_DriverId",
                table: "DayJobs",
                newName: "IX_DayJobs_DriverId");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "TransportInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransportInformations",
                table: "TransportInformations",
                column: "TransportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayJobs",
                table: "DayJobs",
                column: "DayJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LicensePlate",
                table: "Vehicles",
                column: "LicensePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransportInformations_VehicleId",
                table: "TransportInformations",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DayJobs_AspNetUsers_DriverId",
                table: "DayJobs",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportInformations_DayJobs_DayJobId",
                table: "TransportInformations",
                column: "DayJobId",
                principalTable: "DayJobs",
                principalColumn: "DayJobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportInformations_RouteInformations_RouteId",
                table: "TransportInformations",
                column: "RouteId",
                principalTable: "RouteInformations",
                principalColumn: "RouteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportInformations_Vehicles_VehicleId",
                table: "TransportInformations",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayJobs_AspNetUsers_DriverId",
                table: "DayJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportInformations_DayJobs_DayJobId",
                table: "TransportInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportInformations_RouteInformations_RouteId",
                table: "TransportInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportInformations_Vehicles_VehicleId",
                table: "TransportInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_LicensePlate",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransportInformations",
                table: "TransportInformations");

            migrationBuilder.DropIndex(
                name: "IX_TransportInformations_VehicleId",
                table: "TransportInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayJobs",
                table: "DayJobs");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "TransportInformations");

            migrationBuilder.RenameTable(
                name: "TransportInformations",
                newName: "TransportInformation");

            migrationBuilder.RenameTable(
                name: "DayJobs",
                newName: "DayJob");

            migrationBuilder.RenameIndex(
                name: "IX_TransportInformations_RouteId",
                table: "TransportInformation",
                newName: "IX_TransportInformation_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_TransportInformations_DayJobId",
                table: "TransportInformation",
                newName: "IX_TransportInformation_DayJobId");

            migrationBuilder.RenameIndex(
                name: "IX_DayJobs_DriverId",
                table: "DayJob",
                newName: "IX_DayJob_DriverId");

            migrationBuilder.AddColumn<string>(
                name: "VehicleLicensePlate",
                table: "TransportInformation",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "LicensePlate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransportInformation",
                table: "TransportInformation",
                column: "TransportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayJob",
                table: "DayJob",
                column: "DayJobId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportInformation_VehicleLicensePlate",
                table: "TransportInformation",
                column: "VehicleLicensePlate");

            migrationBuilder.AddForeignKey(
                name: "FK_DayJob_AspNetUsers_DriverId",
                table: "DayJob",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportInformation_DayJob_DayJobId",
                table: "TransportInformation",
                column: "DayJobId",
                principalTable: "DayJob",
                principalColumn: "DayJobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportInformation_RouteInformations_RouteId",
                table: "TransportInformation",
                column: "RouteId",
                principalTable: "RouteInformations",
                principalColumn: "RouteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportInformation_Vehicles_VehicleLicensePlate",
                table: "TransportInformation",
                column: "VehicleLicensePlate",
                principalTable: "Vehicles",
                principalColumn: "LicensePlate",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
