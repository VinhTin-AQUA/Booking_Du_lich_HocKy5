using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class AddBookTourDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookTour",
                table: "BookTour");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "BookTour",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureDate",
                table: "BookTour",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "BookTourId",
                table: "BookTour",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "SpecialRequirements",
                table: "BookTour",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookTour",
                table: "BookTour",
                column: "BookTourId");

            migrationBuilder.CreateTable(
                name: "BookTourDetail",
                columns: table => new
                {
                    TicketCode = table.Column<int>(type: "int", nullable: false),
                    BookTourId = table.Column<int>(type: "int", nullable: false),
                    FirstNameTourist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameTourist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdult = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTourDetail", x => new { x.BookTourId, x.TicketCode });
                    table.ForeignKey(
                        name: "FK_BookTourDetail_BookTour_BookTourId",
                        column: x => x.BookTourId,
                        principalTable: "BookTour",
                        principalColumn: "BookTourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookTour_PackageId",
                table: "BookTour",
                column: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTourDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookTour",
                table: "BookTour");

            migrationBuilder.DropIndex(
                name: "IX_BookTour_PackageId",
                table: "BookTour");

            migrationBuilder.DropColumn(
                name: "BookTourId",
                table: "BookTour");

            migrationBuilder.DropColumn(
                name: "SpecialRequirements",
                table: "BookTour");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "BookTour",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureDate",
                table: "BookTour",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookTour",
                table: "BookTour",
                columns: new[] { "PackageId", "UserID", "DepartureDate" });
        }
    }
}
