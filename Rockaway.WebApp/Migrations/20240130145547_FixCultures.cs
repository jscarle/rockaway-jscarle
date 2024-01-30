using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rockaway.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class FixCultures : Migration
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
            foreach (var (countryCode, cultureName) in countryCodesToCultureNames) {
                var sql = $@"
                    UPDATE Venue
                    SET CultureName = '{cultureName}'
                    WHERE CultureName = '{countryCode}'";
                migrationBuilder.Sql(sql);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var (countryCode, cultureName) in countryCodesToCultureNames) {
                var sql = $@"
                    UPDATE Venue
                    SET CultureName = '{countryCode}'
                    WHERE CultureName = '{cultureName}'";
                migrationBuilder.Sql(sql);
            }
        }
    }
}
