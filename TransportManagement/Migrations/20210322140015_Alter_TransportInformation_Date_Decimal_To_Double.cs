using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Alter_TransportInformation_Date_Decimal_To_Double : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DateStartUTC",
                table: "TransportInformations",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<double>(
                name: "DateStartLocal",
                table: "TransportInformations",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<double>(
                name: "DateCompletedUTC",
                table: "TransportInformations",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<double>(
                name: "DateCompletedLocal",
                table: "TransportInformations",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dd36636-b4d8-4010-8594-caebfbe55991",
                column: "ConcurrencyStamp",
                value: "6db497c1-e8f6-4ba7-9bd4-5982e609126e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84572bc3-25fc-4ef8-9056-67c4da04069b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb7f03dd-9a52-4ca9-9da0-cda0223e3546", "AQAAAAEAACcQAAAAEKt0TuZEfc9BBZjosozC5rjAqPQHmaSquLVraP4uT4tbRlzWPl3yeAr012DJQq1PPA==", "856f3b12-8266-4c83-ada5-7c4919a43942" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DateStartUTC",
                table: "TransportInformations",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "DateStartLocal",
                table: "TransportInformations",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "DateCompletedUTC",
                table: "TransportInformations",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "DateCompletedLocal",
                table: "TransportInformations",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dd36636-b4d8-4010-8594-caebfbe55991",
                column: "ConcurrencyStamp",
                value: "3d64e21f-9d31-40d7-aad7-8816448ff5dd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84572bc3-25fc-4ef8-9056-67c4da04069b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2cbb778-543d-42cc-a1e6-13c7f2670687", "AQAAAAEAACcQAAAAEF83H5UdFLLVraRjrbeWrV6dcGZY81tTUjwYajBbLE1MTzyup/u5Axr9usudvs2Plg==", "94063a11-8a2e-4af6-a8e6-dab5598c740c" });
        }
    }
}
