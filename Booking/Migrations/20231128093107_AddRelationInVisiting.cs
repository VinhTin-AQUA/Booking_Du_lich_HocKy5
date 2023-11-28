using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationInVisiting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddForeignKey(
                name: "FK_Visitings_Tour_TourId",
                table: "Visitings",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "TourId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitings_TouristAttraction_TouristAttractionId",
                table: "Visitings",
                column: "TouristAttractionId",
                principalTable: "TouristAttraction",
                principalColumn: "TouristAttractionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitings_Tour_TourId",
                table: "Visitings");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitings_TouristAttraction_TouristAttractionId",
                table: "Visitings");

           
        }
    }
}
