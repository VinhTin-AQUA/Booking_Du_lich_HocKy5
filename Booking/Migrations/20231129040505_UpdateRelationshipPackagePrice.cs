using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipPackagePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PackagePrice_PackageId",
                table: "PackagePrice");

            migrationBuilder.CreateIndex(
                name: "IX_PackagePrice_PackageId",
                table: "PackagePrice",
                column: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PackagePrice_PackageId",
                table: "PackagePrice");

            migrationBuilder.CreateIndex(
                name: "IX_PackagePrice_PackageId",
                table: "PackagePrice",
                column: "PackageId",
                unique: true);
        }
    }
}
