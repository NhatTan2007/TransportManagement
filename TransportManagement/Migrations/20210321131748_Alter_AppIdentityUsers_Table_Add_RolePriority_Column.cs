using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Alter_AppIdentityUsers_Table_Add_RolePriority_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolePriority",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RolePriority",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dd36636-b4d8-4010-8594-caebfbe55991",
                column: "ConcurrencyStamp",
                value: "64bd6762-7f0a-496a-82a4-fffbabe7ba38");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84572bc3-25fc-4ef8-9056-67c4da04069b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b3ec319-c005-4945-9bf4-5aba468a4b56", "AQAAAAEAACcQAAAAEJ58Tab6s27I/6iv1vPCgdZSDGeAddm+51R0ygfNqZd/Y6CYgohnxLck79phlCshlA==", "45e4aba9-fb17-4969-b2f4-531e4212e5e4" });
        }
    }
}
