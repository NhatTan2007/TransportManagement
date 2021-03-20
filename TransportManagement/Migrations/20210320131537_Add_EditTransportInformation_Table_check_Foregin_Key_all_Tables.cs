using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Add_EditTransportInformation_Table_check_Foregin_Key_all_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "UserCreateId",
                table: "TransportInformations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EditTransportInformation",
                columns: table => new
                {
                    EditId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EditContent = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEditUTC = table.Column<double>(type: "float", nullable: false),
                    DateEditLocal = table.Column<double>(type: "float", nullable: false),
                    UserEditId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransportId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportInfoTransportId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditTransportInformation", x => x.EditId);
                    table.ForeignKey(
                        name: "FK_EditTransportInformation_AspNetUsers_UserEditId",
                        column: x => x.UserEditId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EditTransportInformation_TransportInformations_TransportInfoTransportId",
                        column: x => x.TransportInfoTransportId,
                        principalTable: "TransportInformations",
                        principalColumn: "TransportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportInformations_UserCreateId",
                table: "TransportInformations",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_EditTransportInformation_TransportInfoTransportId",
                table: "EditTransportInformation",
                column: "TransportInfoTransportId");

            migrationBuilder.CreateIndex(
                name: "IX_EditTransportInformation_UserEditId",
                table: "EditTransportInformation",
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
                name: "EditTransportInformation");

            migrationBuilder.DropIndex(
                name: "IX_TransportInformations_UserCreateId",
                table: "TransportInformations");

            migrationBuilder.DropColumn(
                name: "UserCreateId",
                table: "TransportInformations");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
