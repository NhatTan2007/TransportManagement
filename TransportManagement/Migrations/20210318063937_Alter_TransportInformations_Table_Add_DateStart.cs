using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Alter_TransportInformations_Table_Add_DateStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeCompleted",
                table: "TransportInformations");

            migrationBuilder.AlterColumn<string>(
                name: "ReasonCancel",
                table: "TransportInformations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateStart",
                table: "TransportInformations",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "dateCompleted",
                table: "TransportInformations",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "TransportInformations");

            migrationBuilder.DropColumn(
                name: "dateCompleted",
                table: "TransportInformations");

            migrationBuilder.AlterColumn<string>(
                name: "ReasonCancel",
                table: "TransportInformations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeCompleted",
                table: "TransportInformations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
