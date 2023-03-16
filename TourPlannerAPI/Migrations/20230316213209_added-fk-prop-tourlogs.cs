using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedfkproptourlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourLog_Tours_TourId",
                table: "TourLog");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "TourLog",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TourLog_Tours_TourId",
                table: "TourLog",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "TourId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourLog_Tours_TourId",
                table: "TourLog");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "TourLog",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_TourLog_Tours_TourId",
                table: "TourLog",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "TourId");
        }
    }
}
