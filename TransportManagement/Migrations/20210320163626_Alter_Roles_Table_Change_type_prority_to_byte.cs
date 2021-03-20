using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Alter_Roles_Table_Change_type_prority_to_byte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditTransportInformation_AspNetUsers_UserEditId",
                table: "EditTransportInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_EditTransportInformation_TransportInformations_TransportInfoTransportId",
                table: "EditTransportInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EditTransportInformation",
                table: "EditTransportInformation");

            migrationBuilder.RenameTable(
                name: "EditTransportInformation",
                newName: "EditTransportInformations");

            migrationBuilder.RenameIndex(
                name: "IX_EditTransportInformation_UserEditId",
                table: "EditTransportInformations",
                newName: "IX_EditTransportInformations_UserEditId");

            migrationBuilder.RenameIndex(
                name: "IX_EditTransportInformation_TransportInfoTransportId",
                table: "EditTransportInformations",
                newName: "IX_EditTransportInformations_TransportInfoTransportId");

            migrationBuilder.AlterColumn<byte>(
                name: "RolePriority",
                table: "AspNetRoles",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EditTransportInformations",
                table: "EditTransportInformations",
                column: "EditId");

            migrationBuilder.AddForeignKey(
                name: "FK_EditTransportInformations_AspNetUsers_UserEditId",
                table: "EditTransportInformations",
                column: "UserEditId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EditTransportInformations_TransportInformations_TransportInfoTransportId",
                table: "EditTransportInformations",
                column: "TransportInfoTransportId",
                principalTable: "TransportInformations",
                principalColumn: "TransportId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditTransportInformations_AspNetUsers_UserEditId",
                table: "EditTransportInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_EditTransportInformations_TransportInformations_TransportInfoTransportId",
                table: "EditTransportInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EditTransportInformations",
                table: "EditTransportInformations");

            migrationBuilder.RenameTable(
                name: "EditTransportInformations",
                newName: "EditTransportInformation");

            migrationBuilder.RenameIndex(
                name: "IX_EditTransportInformations_UserEditId",
                table: "EditTransportInformation",
                newName: "IX_EditTransportInformation_UserEditId");

            migrationBuilder.RenameIndex(
                name: "IX_EditTransportInformations_TransportInfoTransportId",
                table: "EditTransportInformation",
                newName: "IX_EditTransportInformation_TransportInfoTransportId");

            migrationBuilder.AlterColumn<string>(
                name: "RolePriority",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EditTransportInformation",
                table: "EditTransportInformation",
                column: "EditId");

            migrationBuilder.AddForeignKey(
                name: "FK_EditTransportInformation_AspNetUsers_UserEditId",
                table: "EditTransportInformation",
                column: "UserEditId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EditTransportInformation_TransportInformations_TransportInfoTransportId",
                table: "EditTransportInformation",
                column: "TransportInfoTransportId",
                principalTable: "TransportInformations",
                principalColumn: "TransportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
