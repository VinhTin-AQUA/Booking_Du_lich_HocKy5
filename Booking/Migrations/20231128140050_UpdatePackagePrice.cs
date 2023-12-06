using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePackagePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PackagePrice",
                table: "PackagePrice");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidFrom",
                table: "PackagePrice",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "PackagePrice",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackagePrice",
                table: "PackagePrice",
                column: "PriceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PackagePrice",
                table: "PackagePrice");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "PackagePrice");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidFrom",
                table: "PackagePrice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackagePrice",
                table: "PackagePrice",
                columns: new[] { "PackageId", "ValidFrom" });
        }
    }
}
