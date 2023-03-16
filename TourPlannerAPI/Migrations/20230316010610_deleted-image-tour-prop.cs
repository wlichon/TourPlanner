using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class deletedimagetourprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "TourInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "TourInfo",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
