using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipSthing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_Category_TourTypeId",
                table: "Tour");

            migrationBuilder.DropIndex(
                name: "IX_Tour_TourTypeId",
                table: "Tour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tour_TourTypeId",
                table: "Tour",
                column: "TourTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_Category_TourTypeId",
                table: "Tour",
                column: "TourTypeId",
                principalTable: "Category",
                principalColumn: "CategoryId");
        }
    }
}
