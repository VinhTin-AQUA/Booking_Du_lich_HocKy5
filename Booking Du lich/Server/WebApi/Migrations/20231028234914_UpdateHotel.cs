using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class UpdateHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "Hotel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApproverID",
                table: "Hotel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosterID",
                table: "Hotel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostingDate",
                table: "Hotel",
                type: "datetime2",
                nullable: true);

            

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Users_ApproverID",
                table: "Hotel",
                column: "ApproverID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Users_PosterID",
                table: "Hotel",
                column: "PosterID",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Users_ApproverID",
                table: "Hotel");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Users_PosterID",
                table: "Hotel");

           

            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "ApproverID",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "PosterID",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "PostingDate",
                table: "Hotel");
        }
    }
}
