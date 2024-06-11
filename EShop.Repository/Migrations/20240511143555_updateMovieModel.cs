using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Repository.Migrations
{
    public partial class updateMovieModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "MovieCoverImage",
                table: "Movies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "MovieDuration",
                table: "Movies",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MovieYear",
                table: "Movies",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieCoverImage",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieDuration",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieYear",
                table: "Movies");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
