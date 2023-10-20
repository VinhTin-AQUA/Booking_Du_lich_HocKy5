using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class UpdateCityModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_City_CityId",
                table: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Hotel_CityId",
                table: "Hotel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                table: "Hotel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CityCode",
                table: "City",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                columns: new[] { "Id", "CityCode" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_City_CityId_CityCode",
                table: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Hotel_CityId_CityCode",
                table: "Hotel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "769b79ed-ff6e-4ac3-83eb-04e5f10db4f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0483baa-b3b7-41a0-8e44-0faa14f9c37a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4dd10a9-b45e-4954-9759-f3e79a5fc934");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed92ea7a-7051-478a-b5ac-0270ab0eb5c1");

            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "Hotel");

            migrationBuilder.AlterColumn<int>(
                name: "CityCode",
                table: "City",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "Id");

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
        }
    }
}
