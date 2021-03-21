using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Add_Seed_User_Data_Add_EditTransportInformation_Table_Check_All_Foregin_Key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsingFrom",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "TransportInformations",
                newName: "DateStartUTC");

            migrationBuilder.RenameColumn(
                name: "DateCompleted",
                table: "TransportInformations",
                newName: "DateStartLocal");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vehicles",
                type: "bit",
                maxLength: 10,
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddColumn<string>(
                name: "UserCreateId",
                table: "TransportInformations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "RolePriority",
                table: "AspNetRoles",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EditTransportInformations",
                columns: table => new
                {
                    EditId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EditContent = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEditUTC = table.Column<double>(type: "float", nullable: false),
                    DateEditLocal = table.Column<double>(type: "float", nullable: false),
                    UserEditId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TransportId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditTransportInformations", x => x.EditId);
                    table.ForeignKey(
                        name: "FK_EditTransportInformations_AspNetUsers_UserEditId",
                        column: x => x.UserEditId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EditTransportInformations_TransportInformations_TransportId",
                        column: x => x.TransportId,
                        principalTable: "TransportInformations",
                        principalColumn: "TransportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsActive", "Name", "NormalizedName", "RolePriority" },
                values: new object[] { "8dd36636-b4d8-4010-8594-caebfbe55991", "64bd6762-7f0a-496a-82a4-fffbabe7ba38", true, "Quản trị viên hệ thống", "QUẢN TRỊ VIÊN HỆ THỐNG", (byte)1 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Department", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsAvailable", "JobTitle", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "84572bc3-25fc-4ef8-9056-67c4da04069b", 0, "noavatar.png", "9b3ec319-c005-4945-9bf4-5aba468a4b56", null, null, false, "Administrator", true, true, null, "System", false, null, null, null, "ADMIN", "AQAAAAEAACcQAAAAEJ58Tab6s27I/6iv1vPCgdZSDGeAddm+51R0ygfNqZd/Y6CYgohnxLck79phlCshlA==", "0911345992", true, "45e4aba9-fb17-4969-b2f4-531e4212e5e4", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8dd36636-b4d8-4010-8594-caebfbe55991", "84572bc3-25fc-4ef8-9056-67c4da04069b" });

            migrationBuilder.CreateIndex(
                name: "IX_TransportInformations_UserCreateId",
                table: "TransportInformations",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_EditTransportInformations_TransportId",
                table: "EditTransportInformations",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_EditTransportInformations_UserEditId",
                table: "EditTransportInformations",
                column: "UserEditId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportInformations_AspNetUsers_UserCreateId",
                table: "TransportInformations",
                column: "UserCreateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportInformations_AspNetUsers_UserCreateId",
                table: "TransportInformations");

            migrationBuilder.DropTable(
                name: "EditTransportInformations");

            migrationBuilder.DropIndex(
                name: "IX_TransportInformations_UserCreateId",
                table: "TransportInformations");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8dd36636-b4d8-4010-8594-caebfbe55991", "84572bc3-25fc-4ef8-9056-67c4da04069b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8dd36636-b4d8-4010-8594-caebfbe55991");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "84572bc3-25fc-4ef8-9056-67c4da04069b");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DateCompletedLocal",
                table: "TransportInformations");

            migrationBuilder.DropColumn(
                name: "DateCompletedUTC",
                table: "TransportInformations");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "TransportInformations");

            migrationBuilder.DropColumn(
                name: "UserCreateId",
                table: "TransportInformations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "DateStartUTC",
                table: "TransportInformations",
                newName: "DateStart");

            migrationBuilder.RenameColumn(
                name: "DateStartLocal",
                table: "TransportInformations",
                newName: "DateCompleted");

            migrationBuilder.AddColumn<string>(
                name: "UsingFrom",
                table: "Vehicles",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RolePriority",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
