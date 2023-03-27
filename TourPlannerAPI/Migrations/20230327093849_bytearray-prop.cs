using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class bytearrayprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_TourInfo_TourInfoId",
                table: "Tours");

            migrationBuilder.AlterColumn<int>(
                name: "TourInfoId",
                table: "Tours",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "TourInfo",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_TourInfo_TourInfoId",
                table: "Tours",
                column: "TourInfoId",
                principalTable: "TourInfo",
                principalColumn: "TourInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_TourInfo_TourInfoId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "TourInfo");

            migrationBuilder.AlterColumn<int>(
                name: "TourInfoId",
                table: "Tours",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_TourInfo_TourInfoId",
                table: "Tours",
                column: "TourInfoId",
                principalTable: "TourInfo",
                principalColumn: "TourInfoId");
        }
    }
}
