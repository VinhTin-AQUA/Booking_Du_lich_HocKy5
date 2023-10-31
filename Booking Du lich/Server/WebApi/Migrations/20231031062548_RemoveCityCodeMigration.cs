using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class RemoveCityCodeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_City_CityId_CityCode",
                table: "Hotel");

            migrationBuilder.DropForeignKey(
                name: "FK_Tour_City_CityId_CityCode",
                table: "Tour");



            migrationBuilder.DropIndex(
                name: "IX_Hotel_CityId_CityCode",
                table: "Hotel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "Tour");

            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "City");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_CityId",
                table: "Tour",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_CityId",
                table: "Hotel",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_City_CityId",
                table: "Hotel",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_City_CityId",
                table: "Tour",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_City_CityId",
                table: "Hotel");

            migrationBuilder.DropForeignKey(
                name: "FK_Tour_City_CityId",
                table: "Tour");

            migrationBuilder.DropIndex(
                name: "IX_Tour_CityId",
                table: "Tour");

            migrationBuilder.DropIndex(
                name: "IX_Hotel_CityId",
                table: "Hotel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "Tour",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "Hotel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "City",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                columns: new[] { "Id", "CityCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Tour_CityId_CityCode",
                table: "Tour",
                columns: new[] { "CityId", "CityCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_CityId_CityCode",
                table: "Hotel",
                columns: new[] { "CityId", "CityCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_City_CityId_CityCode",
                table: "Hotel",
                columns: new[] { "CityId", "CityCode" },
                principalTable: "City",
                principalColumns: new[] { "Id", "CityCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_City_CityId_CityCode",
                table: "Tour",
                columns: new[] { "CityId", "CityCode" },
                principalTable: "City",
                principalColumns: new[] { "Id", "CityCode" });
        }
    }
}
