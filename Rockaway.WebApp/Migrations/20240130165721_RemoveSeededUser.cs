using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rockaway.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeededUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "rockaway-sample-admin-user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "rockaway-sample-admin-user", 0, "49f40a73-8b96-4baa-bf16-2f48c4997c62", "admin@rockaway.dev", true, true, null, "ADMIN@ROCKAWAY.DEV", "ADMIN@ROCKAWAY.DEV", "AQAAAAIAAYagAAAAEMzhy3c7yU5CYVlv2V6mA+VqxeSaxzfyzyvPq/pl/D1Ghk3FPDaK14tRGMJD6WoIGg==", null, true, "ecb9640b-a274-4b66-a685-731c3cfea082", false, "admin@rockaway.dev" });
        }
    }
}
