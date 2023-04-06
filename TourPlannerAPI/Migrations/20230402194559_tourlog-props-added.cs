using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class tourlogpropsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "TourLog");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "TourLog",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "TourLog",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "TourLog",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "TourLog",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "TourLog");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "TourLog");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "TourLog");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "TourLog",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Distance",
                table: "TourLog",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
