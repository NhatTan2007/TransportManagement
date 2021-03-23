using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Add_Fuels_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditTransportInformations_TransportInformations_TransportId",
                table: "EditTransportInformations");

            migrationBuilder.AddColumn<int>(
                name: "FuelId",
                table: "Vehicles",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReturnOfAdvances",
                table: "TransportInformations",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CargoTonnage",
                table: "TransportInformations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdvanceMoney",
                table: "TransportInformations",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<string>(
                name: "TimeZone",
                table: "EditTransportInformations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Fuels",
                columns: table => new
                {
                    FuelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FuelPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuels", x => x.FuelId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dd36636-b4d8-4010-8594-caebfbe55991",
                column: "ConcurrencyStamp",
                value: "5abef0a2-8e29-4e00-96c3-10757f2babbc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84572bc3-25fc-4ef8-9056-67c4da04069b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a11d0a3c-45df-463c-b5b9-e04cb0a70583", "AQAAAAEAACcQAAAAEMAQJcrq0MAAR/+TXLoacDQ4OiPk8BRyU9s71T1P28Bw74bUYOU+Wk7WBdzfuuRTPA==", "d1f1401c-602a-4820-8531-34b62b2368c3" });

            migrationBuilder.AddForeignKey(
                name: "FK_EditTransportInformations_TransportInformations_TransportId",
                table: "EditTransportInformations",
                column: "TransportId",
                principalTable: "TransportInformations",
                principalColumn: "TransportId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditTransportInformations_TransportInformations_TransportId",
                table: "EditTransportInformations");

            migrationBuilder.DropTable(
                name: "Fuels");

            migrationBuilder.DropColumn(
                name: "FuelId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReturnOfAdvances",
                table: "TransportInformations",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CargoTonnage",
                table: "TransportInformations",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdvanceMoney",
                table: "TransportInformations",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<string>(
                name: "TimeZone",
                table: "EditTransportInformations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dd36636-b4d8-4010-8594-caebfbe55991",
                column: "ConcurrencyStamp",
                value: "0214610b-55f2-4c31-996c-04c241d70001");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84572bc3-25fc-4ef8-9056-67c4da04069b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f1b5003-6d53-4daa-8f5f-2fd16e176d38", "AQAAAAEAACcQAAAAECbdKZdNlc7jPS4f7Hy4aTD5tMA3p956sC18OHyQBJ4rzR0zJh4dxGFV+VaV+wJPag==", "d625b0d5-514d-4e0b-859a-c76a39062eaf" });

            migrationBuilder.AddForeignKey(
                name: "FK_EditTransportInformations_TransportInformations_TransportId",
                table: "EditTransportInformations",
                column: "TransportId",
                principalTable: "TransportInformations",
                principalColumn: "TransportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
