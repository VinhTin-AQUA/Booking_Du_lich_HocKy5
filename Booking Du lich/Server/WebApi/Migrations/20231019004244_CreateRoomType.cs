using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class CreateRoomType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomTypeId",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Room Type",
                columns: table => new
                {
                    RoomTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeName = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room Type", x => x.RoomTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomTypeId",
                table: "Room",
                column: "RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Room Type_RoomTypeId",
                table: "Room",
                column: "RoomTypeId",
                principalTable: "Room Type",
                principalColumn: "RoomTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Room Type_RoomTypeId",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Room Type");

            migrationBuilder.DropIndex(
                name: "IX_Room_RoomTypeId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "RoomTypeId",
                table: "Room");
        }
    }
}
