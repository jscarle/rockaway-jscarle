﻿using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

public class RockawayDbContext(DbContextOptions<RockawayDbContext> options) : DbContext(options)
{
    public DbSet<Artist> Artists { get; set; } = default!;
    public DbSet<Venue> Venues { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes()) entity.SetTableName(entity.DisplayName());

        modelBuilder.Entity<Artist>(entity => { entity.HasIndex(artist => artist.Slug).IsUnique(); });
        modelBuilder.Entity<Venue>(entity => { entity.HasIndex(venue => venue.Slug).IsUnique(); });

        modelBuilder.Entity<Artist>().HasData(SampleData.Artists.AllArtists);
        modelBuilder.Entity<Venue>().HasData(SampleData.Venues.AllVenues);
    }
}