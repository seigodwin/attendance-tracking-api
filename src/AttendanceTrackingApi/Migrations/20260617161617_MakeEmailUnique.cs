using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTrackingApi.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmailUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "iX_AspNetUsers_email",
                table: "AspNetUsers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_AspNetUsers_phoneNumber",
                table: "AspNetUsers",
                column: "phoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "iX_AspNetUsers_email",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "iX_AspNetUsers_phoneNumber",
                table: "AspNetUsers");
        }
    }
}
