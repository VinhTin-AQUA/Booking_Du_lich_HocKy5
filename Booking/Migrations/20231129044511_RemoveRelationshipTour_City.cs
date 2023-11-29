using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelationshipTour_City : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_City_CityId",
                table: "Tour");

            migrationBuilder.DropIndex(
                name: "IX_Tour_CityId",
                table: "Tour");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Tour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Tour",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tour_CityId",
                table: "Tour",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_City_CityId",
                table: "Tour",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
