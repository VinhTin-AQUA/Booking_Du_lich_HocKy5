using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class UpdateModelRoomCityHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableRoom",
                table: "Hotel");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Room",
                newName: "RoomName");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "City",
                newName: "PhotoPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomName",
                table: "Room",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "City",
                newName: "ImgUrl");

            migrationBuilder.AddColumn<int>(
                name: "AvailableRoom",
                table: "Hotel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
