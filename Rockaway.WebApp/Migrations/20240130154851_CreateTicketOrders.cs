using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rockaway.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateTicketOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowVenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CustomerName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    CustomerEmail = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CompletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketOrder_Show_ShowVenueId_ShowDate",
                        columns: x => new { x.ShowVenueId, x.ShowDate },
                        principalTable: "Show",
                        principalColumns: new[] { "VenueId", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketOrderItem",
                columns: table => new
                {
                    TicketOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOrderItem", x => new { x.TicketOrderId, x.TicketTypeId });
                    table.ForeignKey(
                        name: "FK_TicketOrderItem_TicketOrder_TicketOrderId",
                        column: x => x.TicketOrderId,
                        principalTable: "TicketOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketOrderItem_TicketType_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "rockaway-sample-admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5405dba-da9e-46af-b25e-75911d85289b", "AQAAAAIAAYagAAAAENxZnino01KHfjhmibhBeybYUbeJcpAESe96/itZzAm3JOSmtemwOqCIMAY9BI7EZg==", "d8359c6f-2de7-48d4-8df9-e1d2cc30384d" });

            migrationBuilder.InsertData(
                table: "TicketOrder",
                columns: new[] { "Id", "CompletedAt", "CreatedAt", "CustomerEmail", "CustomerName", "ShowDate", "ShowVenueId" },
                values: new object[,]
                {
                    { new Guid("560ed55e-c635-4f0e-a433-a23ab6fa7bb6"), new DateTimeOffset(new DateTime(2024, 4, 8, 13, 22, 18, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 8, 13, 4, 18, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "brian@example.com", "Brian Johnson", new DateOnly(2024, 5, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9") },
                    { new Guid("ac824d10-367f-494c-ad32-f221420c7c3c"), new DateTimeOffset(new DateTime(2024, 4, 5, 9, 20, 16, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 5, 9, 4, 16, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ace@example.com", "Ace Frehley", new DateOnly(2024, 5, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7") },
                    { new Guid("f584739d-2ec0-4de8-8de2-140333516b4f"), new DateTimeOffset(new DateTime(2024, 4, 11, 10, 16, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 11, 10, 4, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "joey.tempest@example.com", "Joey Tempest", new DateOnly(2024, 5, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8") }
                });

            migrationBuilder.InsertData(
                table: "TicketOrderItem",
                columns: new[] { "TicketOrderId", "TicketTypeId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("560ed55e-c635-4f0e-a433-a23ab6fa7bb6"), new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc8"), 3 },
                    { new Guid("560ed55e-c635-4f0e-a433-a23ab6fa7bb6"), new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc9"), 2 },
                    { new Guid("ac824d10-367f-494c-ad32-f221420c7c3c"), new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc1"), 4 },
                    { new Guid("ac824d10-367f-494c-ad32-f221420c7c3c"), new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc2"), 5 },
                    { new Guid("ac824d10-367f-494c-ad32-f221420c7c3c"), new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc3"), 5 },
                    { new Guid("f584739d-2ec0-4de8-8de2-140333516b4f"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccc12"), 3 },
                    { new Guid("f584739d-2ec0-4de8-8de2-140333516b4f"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccc13"), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketOrder_ShowVenueId_ShowDate",
                table: "TicketOrder",
                columns: new[] { "ShowVenueId", "ShowDate" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketOrderItem_TicketTypeId",
                table: "TicketOrderItem",
                column: "TicketTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketOrderItem");

            migrationBuilder.DropTable(
                name: "TicketOrder");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "rockaway-sample-admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12def4c1-9bcf-4d1f-ac78-83b8df36b17f", "AQAAAAIAAYagAAAAECh4X0pYVOG/EPBqrjhM77DEGpTiqUuNm4v6yBUDOX11XgXNtWrHZsIipp5+o/VTSg==", "3fdc940e-d793-42eb-9a5f-22ad427b23cf" });
        }
    }
}
