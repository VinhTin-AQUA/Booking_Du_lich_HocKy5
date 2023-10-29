using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class CreateTour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schedule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DropOffLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CityCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TourTypeId = table.Column<int>(type: "int", nullable: true),
                    PostingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PosterID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApproverID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.TourId);
                    table.ForeignKey(
                        name: "FK_Tour_City_CityId_CityCode",
                        columns: x => new { x.CityId, x.CityCode },
                        principalTable: "City",
                        principalColumns: new[] { "Id", "CityCode" });
                    table.ForeignKey(
                        name: "FK_Tour_TourType_TourTypeId",
                        column: x => x.TourTypeId,
                        principalTable: "TourType",
                        principalColumn: "TourTypeId");
                    table.ForeignKey(
                        name: "FK_Tour_Users_ApproverID",
                        column: x => x.ApproverID,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tour_Users_PosterID",
                        column: x => x.PosterID,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tour_ApproverID",
                table: "Tour",
                column: "ApproverID");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_CityId_CityCode",
                table: "Tour",
                columns: new[] { "CityId", "CityCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Tour_PosterID",
                table: "Tour",
                column: "PosterID");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_TourTypeId",
                table: "Tour",
                column: "TourTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tour");
        }
    }
}
