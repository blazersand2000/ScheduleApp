using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleApp.Migrations
{
    public partial class cleanup2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Schedule_ScheduleId",
                table: "Shift");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Shift",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Schedule_ScheduleId",
                table: "Shift",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Schedule_ScheduleId",
                table: "Shift");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Shift",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Schedule_ScheduleId",
                table: "Shift",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
