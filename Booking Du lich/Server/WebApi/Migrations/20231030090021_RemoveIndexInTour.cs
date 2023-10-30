using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class RemoveIndexInTour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tour_ApproverID",
                table: "Tour");
            migrationBuilder.DropIndex(
                name: "IX_Tour_CityId_CityCode",
                table: "Tour");
            migrationBuilder.DropIndex(
                name: "IX_Tour_PosterID",
                table: "Tour");
            migrationBuilder.DropIndex(
                name: "IX_Tour_TourTypeId",
                table: "Tour");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
               name: "IX_Tour_ApproverID",
               table: "Tour");
            migrationBuilder.DropIndex(
                name: "IX_Tour_CityId_CityCode",
                table: "Tour");
            migrationBuilder.DropIndex(
                name: "IX_Tour_PosterID",
                table: "Tour");
            migrationBuilder.DropIndex(
                name: "IX_Tour_TourTypeId",
                table: "Tour");
        }
    }
}
