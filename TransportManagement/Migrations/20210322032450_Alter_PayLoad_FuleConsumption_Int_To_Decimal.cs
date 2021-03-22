using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Alter_PayLoad_FuleConsumption_Int_To_Decimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VehiclePayload",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "FuelConsumptionPerTone",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VehiclePayload",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "FuelConsumptionPerTone",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dd36636-b4d8-4010-8594-caebfbe55991",
                column: "ConcurrencyStamp",
                value: "3327e637-14a3-4364-9fb2-48a65721245e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84572bc3-25fc-4ef8-9056-67c4da04069b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d3d81cc-1ef1-446f-bc33-6e07fb70f4b1", "AQAAAAEAACcQAAAAEKD2FgkcwI8BEh2ctt6v52ywJmLMqVjF3KeOwCg8Sn7Hlvi8QJfdGhvIzBBFU49f2A==", "32411c6e-b62c-4855-bed0-37c8a0b9e335" });
        }
    }
}
