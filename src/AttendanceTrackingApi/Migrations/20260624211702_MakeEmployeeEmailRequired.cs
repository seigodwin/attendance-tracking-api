using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTrackingApi.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmployeeEmailRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "staffId",
                table: "employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "id",
                keyValue: 1,
                column: "staffId",
                value: "Ghmis01");

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "id",
                keyValue: 2,
                column: "staffId",
                value: "Ghims02");

            migrationBuilder.UpdateData(
                table: "employees",
                keyColumn: "id",
                keyValue: 3,
                column: "staffId",
                value: "Ghims03");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "staffId",
                table: "employees");
        }
    }
}
