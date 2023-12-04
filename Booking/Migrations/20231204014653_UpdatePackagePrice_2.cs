using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePackagePrice_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PackagePrice",
                newName: "ChildPrice");

            migrationBuilder.AddColumn<double>(
                name: "AdultPrice",
                table: "PackagePrice",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultPrice",
                table: "PackagePrice");

            migrationBuilder.RenameColumn(
                name: "ChildPrice",
                table: "PackagePrice",
                newName: "Price");
        }
    }
}
