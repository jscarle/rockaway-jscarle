using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data.Conventions;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

public class RockawayDbContext(DbContextOptions<RockawayDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Artist> Artists { get; set; } = default!;
    public DbSet<Venue> Venues { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // https://stackoverflow.com/questions/612430/when-must-we-use-nvarchar-nchar-instead-of-varchar-char-in-sql-server#:~:text=Again%2C%20in%20other,the%20UNICODE%20characters.
        modelBuilder.UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

        var rockawayEntityNamespace = typeof(Artist).Namespace;
        var rockawayEntities = modelBuilder.Model.GetEntityTypes().Where(e => e.ClrType.Namespace == rockawayEntityNamespace);
        foreach (var entity in rockawayEntities)
            entity.SetTableName(entity.DisplayName());

        modelBuilder.Entity<Artist>(entity => { entity.HasIndex(artist => artist.Slug).IsUnique(); });
        modelBuilder.Entity<Venue>(entity => { entity.HasIndex(venue => venue.Slug).IsUnique(); });

        modelBuilder.Entity<Artist>().HasData(SampleData.Artists.AllArtists);
        modelBuilder.Entity<Venue>().HasData(SampleData.Venues.AllVenues);
        modelBuilder.Entity<IdentityUser>().HasData(SampleData.Users.Admin);
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(_ => new StringNotUnicodeConvention());
    }
}