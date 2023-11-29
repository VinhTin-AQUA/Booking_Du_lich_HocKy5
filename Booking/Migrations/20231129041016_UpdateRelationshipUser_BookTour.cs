using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipUser_BookTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookTour_UserID",
                table: "BookTour");

            migrationBuilder.CreateIndex(
                name: "IX_BookTour_UserID",
                table: "BookTour",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookTour_UserID",
                table: "BookTour");

            migrationBuilder.CreateIndex(
                name: "IX_BookTour_UserID",
                table: "BookTour",
                column: "UserID",
                unique: true);
        }
    }
}
