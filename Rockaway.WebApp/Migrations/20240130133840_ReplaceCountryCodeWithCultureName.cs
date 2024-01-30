using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rockaway.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceCountryCodeWithCultureName : Migration
    {
        private readonly Dictionary<string, string> countryCodesToCultureNames = new() {
            { "GB", "en-GB" }, // English (Great Britain)
            { "FR", "fr-FR" }, // French (France)
            { "DE", "de-DE" }, // Germany (Germany)
            { "PT", "pt-PT" }, // Portuguese (Portugal)
            { "GR", "el-GR" }, // Greek (Greece)
            { "NO", "nn-NO" }, // Norwegian (Norway)
            { "SE", "sv-SE" }, // Swedish (Sweden)
            { "DK", "dk-DK" } // Danish (Denmark)
        };
        
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CultureName",
                table: "Venue",
                type: "varchar(16)",
                unicode: false,
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            foreach (var (countryCode, cultureName) in countryCodesToCultureNames) {
                var sql = $@"
                    UPDATE Venue
                    SET CultureName = '{cultureName}'
                    WHERE CountryCode = '{countryCode}'";
                migrationBuilder.Sql(sql);
            }

            migrationBuilder.DropColumn(name: "CountryCode", table: "Venue");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "rockaway-sample-admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "481b72cf-86f6-4aa1-b6e1-a7afef02df23", "AQAAAAIAAYagAAAAEIj6LBCaTtxTvfx+D19xZBU3ppZtfUSXyOmgKJRD7AdXhSwkbXwZoDdTEw90qyeqyw==", "b5855459-f092-4d89-b94e-85bbfcb37f5f" });

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                column: "CultureName",
                value: "GB");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                column: "CultureName",
                value: "FR");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                column: "CultureName",
                value: "DE");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                column: "CultureName",
                value: "GR");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"),
                column: "CultureName",
                value: "NO");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb6"),
                column: "CultureName",
                value: "DK");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"),
                column: "CultureName",
                value: "PT");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"),
                column: "CultureName",
                value: "SE");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"),
                column: "CultureName",
                value: "GB");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Venue",
                type: "varchar(2)",
                unicode: false,
                maxLength: 2,
                nullable: false,
                defaultValue: "");
            
            foreach (var (countryCode, cultureName) in countryCodesToCultureNames) {
                var sql = $@"
                    UPDATE Venue
                    SET CountryCode = '{countryCode}'
                    WHERE CultureName = '{cultureName}'";
                migrationBuilder.Sql(sql);
            }
            
            migrationBuilder.DropColumn(
                name: "CultureName",
                table: "Venue");
            
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "rockaway-sample-admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8a6f453-2974-4b10-8e28-22aeda7312ab", "AQAAAAIAAYagAAAAEIGjybML1KAzX7bioAldWq86BGXqx7d1jQrxZyjnt+v1Ikw+b/vvAQrLkoBRB40kUw==", "6f1738e2-154c-4242-9bef-dfbb6ad62ece" });

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                column: "CountryCode",
                value: "GB");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                column: "CountryCode",
                value: "FR");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
                column: "CountryCode",
                value: "DE");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                column: "CountryCode",
                value: "GR");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"),
                column: "CountryCode",
                value: "NO");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb6"),
                column: "CountryCode",
                value: "DK");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"),
                column: "CountryCode",
                value: "PT");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"),
                column: "CountryCode",
                value: "SE");

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"),
                column: "CountryCode",
                value: "GB");
        }
    }
}
