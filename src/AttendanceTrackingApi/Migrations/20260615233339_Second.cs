using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTrackingApi.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "employees",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "id",
                keyValue: 1,
                column: "department",
                value: "IT");

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "id",
                keyValue: 2,
                column: "department",
                value: "Reception");

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "id",
                keyValue: 3,
                column: "department",
                value: "IT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department",
                table: "employees");
        }
    }
}
