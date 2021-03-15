using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportManagement.Migrations
{
    public partial class Create_RouteInformations_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteInformations",
                columns: table => new
                {
                    RouteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeparturePlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    DeparturePlaceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ArrivalPlaceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteInformations", x => x.RouteId);
                    table.ForeignKey(
                        name: "FK_RouteInformations_Locations_ArrivalPlaceId",
                        column: x => x.ArrivalPlaceId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteInformations_Locations_DeparturePlaceId",
                        column: x => x.DeparturePlaceId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteInformations_ArrivalPlaceId",
                table: "RouteInformations",
                column: "ArrivalPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteInformations_DeparturePlaceId",
                table: "RouteInformations",
                column: "DeparturePlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteInformations");
        }
    }
}
