using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AttendanceTrackingApi.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_AspNetRoles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    firstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    userName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    emailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    passwordHash = table.Column<string>(type: "text", nullable: true),
                    securityStamp = table.Column<string>(type: "text", nullable: true),
                    concurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    phoneNumber = table.Column<string>(type: "text", nullable: true),
                    phoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    twoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    accessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_AspNetUsers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_employees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleId = table.Column<string>(type: "text", nullable: false),
                    claimType = table.Column<string>(type: "text", nullable: true),
                    claimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_AspNetRoleClaims", x => x.id);
                    table.ForeignKey(
                        name: "fK_AspNetRoleClaims_AspNetRoles_roleId",
                        column: x => x.roleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<string>(type: "text", nullable: false),
                    claimType = table.Column<string>(type: "text", nullable: true),
                    claimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_AspNetUserClaims", x => x.id);
                    table.ForeignKey(
                        name: "fK_AspNetUserClaims_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    loginProvider = table.Column<string>(type: "text", nullable: false),
                    providerKey = table.Column<string>(type: "text", nullable: false),
                    providerDisplayName = table.Column<string>(type: "text", nullable: true),
                    userId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_AspNetUserLogins", x => new { x.loginProvider, x.providerKey });
                    table.ForeignKey(
                        name: "fK_AspNetUserLogins_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    userId = table.Column<string>(type: "text", nullable: false),
                    roleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_AspNetUserRoles", x => new { x.userId, x.roleId });
                    table.ForeignKey(
                        name: "fK_AspNetUserRoles_AspNetRoles_roleId",
                        column: x => x.roleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_AspNetUserRoles_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    userId = table.Column<string>(type: "text", nullable: false),
                    loginProvider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_AspNetUserTokens", x => new { x.userId, x.loginProvider, x.name });
                    table.ForeignKey(
                        name: "fK_AspNetUserTokens_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "id", "email", "firstName", "lastName", "phoneNumber" },
                values: new object[,]
                {
                    { 1, "seigodwin65@gmail.com", "Sei", "Godwin", "0540580393" },
                    { 2, "ray65@gmail.com", "Sei", "Ray", "0540580393" },
                    { 3, "jane65@gmail.com", "Sei", "Jane", "0540580393" }
                });

            migrationBuilder.CreateIndex(
                name: "iX_AspNetRoleClaims_roleId",
                table: "AspNetRoleClaims",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_AspNetUserClaims_userId",
                table: "AspNetUserClaims",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "iX_AspNetUserLogins_userId",
                table: "AspNetUserLogins",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "iX_AspNetUserRoles_roleId",
                table: "AspNetUserRoles",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_employees_email",
                table: "employees",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
