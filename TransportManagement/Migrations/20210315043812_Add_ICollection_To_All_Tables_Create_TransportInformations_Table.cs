using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Add_ICollection_To_All_Tables_Create_TransportInformations_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayJobs_AspNetUsers_DriverId",
                table: "DayJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayJobs",
                table: "DayJobs");

            migrationBuilder.RenameTable(
                name: "DayJobs",
                newName: "DayJob");

            migrationBuilder.RenameIndex(
                name: "IX_DayJobs_DriverId",
                table: "DayJob",
                newName: "IX_DayJob_DriverId");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleName",
                table: "Vehicles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BrandName",
                table: "VehicleBrands",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DeparturePlace",
                table: "RouteInformations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ArrivalPlace",
                table: "RouteInformations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LocationName",
                table: "Locations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "DayJob",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayJob",
                table: "DayJob",
                column: "DayJobId");

            migrationBuilder.CreateTable(
                name: "TransportInformation",
                columns: table => new
                {
                    TransportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CargoTypes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CargoTonnage = table.Column<int>(type: "int", nullable: false),
                    AdvanceMoney = table.Column<int>(type: "int", nullable: false),
                    ReturnOfAdvances = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    TimeCompleted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false),
                    ReasonCancel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DayJobId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehicleLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RouteId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportInformation", x => x.TransportId);
                    table.ForeignKey(
                        name: "FK_TransportInformation_DayJob_DayJobId",
                        column: x => x.DayJobId,
                        principalTable: "DayJob",
                        principalColumn: "DayJobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportInformation_RouteInformations_RouteId",
                        column: x => x.RouteId,
                        principalTable: "RouteInformations",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportInformation_Vehicles_VehicleLicensePlate",
                        column: x => x.VehicleLicensePlate,
                        principalTable: "Vehicles",
                        principalColumn: "LicensePlate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportInformation_DayJobId",
                table: "TransportInformation",
                column: "DayJobId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportInformation_RouteId",
                table: "TransportInformation",
                column: "RouteId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayJob_AspNetUsers_DriverId",
                table: "DayJob");

            migrationBuilder.DropTable(
                name: "TransportInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayJob",
                table: "DayJob");

            migrationBuilder.RenameTable(
                name: "DayJob",
                newName: "DayJobs");

            migrationBuilder.RenameIndex(
                name: "IX_DayJob_DriverId",
                table: "DayJobs",
                newName: "IX_DayJobs_DriverId");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleName",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "BrandName",
                table: "VehicleBrands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "DeparturePlace",
                table: "RouteInformations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ArrivalPlace",
                table: "RouteInformations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "LocationName",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "DayJobs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayJobs",
                table: "DayJobs",
                column: "DayJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_DayJobs_AspNetUsers_DriverId",
                table: "DayJobs",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
