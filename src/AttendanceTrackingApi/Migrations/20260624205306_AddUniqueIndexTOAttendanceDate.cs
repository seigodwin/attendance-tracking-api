using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTrackingApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexTOAttendanceDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "iX_attendances_attendanceDate",
                table: "attendances",
                column: "attendanceDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "iX_attendances_attendanceDate",
                table: "attendances");
        }
    }
}
