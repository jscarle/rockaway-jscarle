using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data.Conventions;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

public class RockawayDbContext(DbContextOptions<RockawayDbContext> options) : DbContext(options)
{
    public DbSet<Artist> Artists { get; set; } = default!;
    public DbSet<Venue> Venues { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // https://stackoverflow.com/questions/612430/when-must-we-use-nvarchar-nchar-instead-of-varchar-char-in-sql-server#:~:text=Again%2C%20in%20other,the%20UNICODE%20characters.
        modelBuilder.UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

        foreach (var entity in modelBuilder.Model.GetEntityTypes()) entity.SetTableName(entity.DisplayName());

        modelBuilder.Entity<Artist>(entity => { entity.HasIndex(artist => artist.Slug).IsUnique(); });
        modelBuilder.Entity<Venue>(entity => { entity.HasIndex(venue => venue.Slug).IsUnique(); });

        modelBuilder.Entity<Artist>().HasData(SampleData.Artists.AllArtists);
        modelBuilder.Entity<Venue>().HasData(SampleData.Venues.AllVenues);
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(_ => new StringNotUnicodeConvention());
    }
}