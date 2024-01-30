using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rockaway.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class ShowsAndSupportSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WebsiteUrl",
                table: "Venue",
                type: "varchar(4096)",
                unicode: false,
                maxLength: 4096,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Venue",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Venue",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Venue",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.CreateTable(
                name: "Show",
                columns: table => new
                {
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    VenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeadlineArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Show", x => new { x.VenueId, x.Date });
                    table.ForeignKey(
                        name: "FK_Show_Artist_HeadlineArtistId",
                        column: x => x.HeadlineArtistId,
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Show_Venue_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportSlot",
                columns: table => new
                {
                    SlotNumber = table.Column<int>(type: "int", nullable: false),
                    ShowVenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportSlot", x => new { x.ShowVenueId, x.ShowDate, x.SlotNumber });
                    table.ForeignKey(
                        name: "FK_SupportSlot_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportSlot_Show_ShowVenueId_ShowDate",
                        columns: x => new { x.ShowVenueId, x.ShowDate },
                        principalTable: "Show",
                        principalColumns: new[] { "VenueId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "rockaway-sample-admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8a6f453-2974-4b10-8e28-22aeda7312ab", "AQAAAAIAAYagAAAAEIGjybML1KAzX7bioAldWq86BGXqx7d1jQrxZyjnt+v1Ikw+b/vvAQrLkoBRB40kUw==", "6f1738e2-154c-4242-9bef-dfbb6ad62ece" });

            migrationBuilder.InsertData(
                table: "Show",
                columns: new[] { "Date", "VenueId", "HeadlineArtistId" },
                values: new object[,]
                {
                    { new DateOnly(2024, 5, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
                    { new DateOnly(2024, 5, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
                    { new DateOnly(2024, 5, 25), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
                    { new DateOnly(2024, 5, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
                    { new DateOnly(2024, 5, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
                    { new DateOnly(2024, 5, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
                    { new DateOnly(2024, 5, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") }
                });

            migrationBuilder.InsertData(
                table: "SupportSlot",
                columns: new[] { "ShowDate", "ShowVenueId", "SlotNumber", "ArtistId" },
                values: new object[,]
                {
                    { new DateOnly(2024, 5, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa11") },
                    { new DateOnly(2024, 5, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), 2, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa15") },
                    { new DateOnly(2024, 5, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), 3, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10") },
                    { new DateOnly(2024, 5, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa11") },
                    { new DateOnly(2024, 5, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"), 2, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa15") },
                    { new DateOnly(2024, 5, 25), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10") },
                    { new DateOnly(2024, 5, 25), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"), 2, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa19") },
                    { new DateOnly(2024, 5, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10") },
                    { new DateOnly(2024, 5, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa11") },
                    { new DateOnly(2024, 5, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"), 2, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa15") },
                    { new DateOnly(2024, 5, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10") },
                    { new DateOnly(2024, 5, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Show_HeadlineArtistId",
                table: "Show",
                column: "HeadlineArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportSlot_ArtistId",
                table: "SupportSlot",
                column: "ArtistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportSlot");

            migrationBuilder.DropTable(
                name: "Show");

            migrationBuilder.AlterColumn<string>(
                name: "WebsiteUrl",
                table: "Venue",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(4096)",
                oldUnicode: false,
                oldMaxLength: 4096,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telephone",
                table: "Venue",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Venue",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Venue",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "rockaway-sample-admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afee3fe5-9c6e-455a-885d-d0ac0a303b65", "AQAAAAIAAYagAAAAEDPhcP+nLZVOWyLmkquqMty/O1uU4Hzdewp41dM12/BAf/afz9k02qDJYDcAD2+tTw==", "21926617-f9a8-4f73-b987-0ff39e0c43bd" });
        }
    }
}
