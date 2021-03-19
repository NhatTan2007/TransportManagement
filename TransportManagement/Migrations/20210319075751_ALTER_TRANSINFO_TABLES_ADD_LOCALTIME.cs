using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class ALTER_TRANSINFO_TABLES_ADD_LOCALTIME : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "TransportInformations",
                newName: "DateStartUTC");

            migrationBuilder.RenameColumn(
                name: "DateCompleted",
                table: "TransportInformations",
                newName: "DateStartLocal");

            migrationBuilder.AddColumn<decimal>(
                name: "DateCompletedLocal",
                table: "TransportInformations",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DateCompletedUTC",
                table: "TransportInformations",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "TransportInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCompletedLocal",
                table: "TransportInformations");

            migrationBuilder.DropColumn(
                name: "DateCompletedUTC",
                table: "TransportInformations");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "TransportInformations");

            migrationBuilder.RenameColumn(
                name: "DateStartUTC",
                table: "TransportInformations",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "DateStartLocal",
                table: "TransportInformations",
                newName: "DateCompleted");
        }
    }
}
