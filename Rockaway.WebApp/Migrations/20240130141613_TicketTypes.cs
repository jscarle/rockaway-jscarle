using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rockaway.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class TicketTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowVenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketType_Show_ShowVenueId_ShowDate",
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
                values: new object[] { "8c5e3d01-032f-4a90-bff4-28d1ff1876d1", "AQAAAAIAAYagAAAAEL9uD2lVZzw1oSz9kwgR/mkpNTirHPcjCaILGRdwF9m7KBR54MJ9Ho/sIGjjSBVlgA==", "2167fd59-afa5-4855-9ebb-ebc329217842" });

            migrationBuilder.InsertData(
                table: "TicketType",
                columns: new[] { "Id", "Limit", "Name", "Price", "ShowDate", "ShowVenueId" },
                values: new object[,]
                {
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc10"), null, "General Admission", 350m, new DateOnly(2024, 5, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc11"), null, "VIP Meet & Greet", 750m, new DateOnly(2024, 5, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc12"), null, "General Admission", 300m, new DateOnly(2024, 5, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc13"), null, "VIP Meet & Greet", 720m, new DateOnly(2024, 5, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc14"), null, "General Admission", 25m, new DateOnly(2024, 5, 25), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc1"), null, "Upstairs unallocated seating", 25m, new DateOnly(2024, 5, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc2"), null, "Downstairs standing", 25m, new DateOnly(2024, 5, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc3"), null, "Cabaret table (4 people)", 120m, new DateOnly(2024, 5, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc4"), null, "General Admission", 35m, new DateOnly(2024, 5, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc5"), null, "VIP Meet & Greet", 75m, new DateOnly(2024, 5, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc6"), null, "General Admission", 35m, new DateOnly(2024, 5, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc7"), null, "VIP Meet & Greet", 75m, new DateOnly(2024, 5, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc8"), null, "General Admission", 25m, new DateOnly(2024, 5, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc9"), null, "VIP Meet & Greet", 55m, new DateOnly(2024, 5, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketType_ShowVenueId_ShowDate",
                table: "TicketType",
                columns: new[] { "ShowVenueId", "ShowDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketType");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "rockaway-sample-admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "481b72cf-86f6-4aa1-b6e1-a7afef02df23", "AQAAAAIAAYagAAAAEIj6LBCaTtxTvfx+D19xZBU3ppZtfUSXyOmgKJRD7AdXhSwkbXwZoDdTEw90qyeqyw==", "b5855459-f092-4d89-b94e-85bbfcb37f5f" });
        }
    }
}
