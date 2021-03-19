using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class ALTER_TRANSINFOR_TABLE_CHANGE_TYPE_ADVANCEMONEY_TO_DECIMAL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateCompleted",
                table: "TransportInformations",
                newName: "DateCompleted");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReturnOfAdvances",
                table: "TransportInformations",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdvanceMoney",
                table: "TransportInformations",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCompleted",
                table: "TransportInformations",
                newName: "dateCompleted");

            migrationBuilder.AlterColumn<int>(
                name: "ReturnOfAdvances",
                table: "TransportInformations",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<int>(
                name: "AdvanceMoney",
                table: "TransportInformations",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");
        }
    }
}
