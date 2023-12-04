using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVisitingAndTouristAttraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visitings");

            migrationBuilder.DropTable(
                name: "TouristAttraction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TouristAttraction",
                columns: table => new
                {
                    TouristAttractionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    TouristAttractionName = table.Column<string>(type: "nvarchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristAttraction", x => x.TouristAttractionId);
                    table.ForeignKey(
                        name: "FK_TouristAttraction_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Visitings",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "int", nullable: false),
                    TouristAttractionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitings", x => new { x.TourId, x.TouristAttractionId });
                    table.ForeignKey(
                        name: "FK_Visitings_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitings_TouristAttraction_TouristAttractionId",
                        column: x => x.TouristAttractionId,
                        principalTable: "TouristAttraction",
                        principalColumn: "TouristAttractionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttraction_CityId",
                table: "TouristAttraction",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitings_TouristAttractionId",
                table: "Visitings",
                column: "TouristAttractionId");
        }
    }
}
