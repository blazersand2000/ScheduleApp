using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleApp.Migrations
{
    public partial class AddShiftRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShiftRoleId",
                table: "Shift",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShiftRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftRole_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shift_ShiftRoleId",
                table: "Shift",
                column: "ShiftRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRole_ScheduleId",
                table: "ShiftRole",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_ShiftRole_ShiftRoleId",
                table: "Shift",
                column: "ShiftRoleId",
                principalTable: "ShiftRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_ShiftRole_ShiftRoleId",
                table: "Shift");

            migrationBuilder.DropTable(
                name: "ShiftRole");

            migrationBuilder.DropIndex(
                name: "IX_Shift_ShiftRoleId",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "ShiftRoleId",
                table: "Shift");
        }
    }
}
