using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class RemoveIndexInBookRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Book Room_RoomID",
                table: "Book Room");
            migrationBuilder.DropIndex(
                name: "IX_Book Room_UserID",
                table: "Book Room");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Book Room_RoomID",
                table: "Book Room");
            migrationBuilder.DropIndex(
                name: "IX_Book Room_UserID",
                table: "Book Room");
        }
    }
}
