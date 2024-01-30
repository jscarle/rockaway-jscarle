﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data.Conventions;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

// ReSharper disable InvokeAsExtensionMethod
public sealed class RockawayDbContext(DbContextOptions<RockawayDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Artist> Artists { get; set; } = default!;
    public DbSet<Venue> Venues { get; set; } = default!;
    public DbSet<Show> Shows { get; set; } = default!;
    public DbSet<TicketOrder> TicketOrders { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // https://stackoverflow.com/questions/612430/when-must-we-use-nvarchar-nchar-instead-of-varchar-char-in-sql-server#:~:text=Again%2C%20in%20other,the%20UNICODE%20characters.
        modelBuilder.UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

        var rockawayEntityNamespace = typeof(Artist).Namespace;
        var rockawayEntities = modelBuilder.Model.GetEntityTypes()
            .Where(e => e.ClrType.Namespace == rockawayEntityNamespace);
        foreach (var entity in rockawayEntities)
            entity.SetTableName(entity.DisplayName());

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasIndex(artist => artist.Slug).IsUnique();
            entity.HasMany(a => a.HeadlineShows)
                .WithOne(s => s.HeadlineArtist)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasIndex(venue => venue.Slug).IsUnique();
            entity.HasMany(v => v.Shows)
                .WithOne(s => s.Venue)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Show>(entity =>
        {
            EntityTypeBuilderExtensions.HasKey(entity, show => show.Venue.Id, show => show.Date);
            entity.HasMany(show => show.SupportSlots)
                .WithOne(ss => ss.Show).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(show => show.TicketTypes)
                .WithOne(tt => tt.Show).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(show => show.TicketOrders)
                .WithOne(to => to.Show).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SupportSlot>(entity =>
        {
            EntityTypeBuilderExtensions.HasKey(entity, slot => slot.Show.Venue.Id,
                slot => slot.Show.Date,
                slot => slot.SlotNumber
            );
        });

        modelBuilder.Entity<TicketType>(entity => { entity.Property(tt => tt.Price).HasColumnType("money"); });

        modelBuilder.Entity<TicketOrderItem>(entity =>
        {
            EntityTypeBuilderExtensions.HasKey(entity,
                toi => toi.TicketOrder.Id,
                toi => toi.TicketType.Id
            );
        });

        modelBuilder.Entity<Artist>()
            .HasData(SeedData.For(SampleData.Artists.AllArtists));
        modelBuilder.Entity<Venue>()
            .HasData(SeedData.For(SampleData.Venues.AllVenues));
        modelBuilder.Entity<Show>()
            .HasData(SeedData.For(SampleData.Shows.AllShows));
        modelBuilder.Entity<SupportSlot>()
            .HasData(SeedData.For(SampleData.Shows.AllSupportSlots));
        modelBuilder.Entity<TicketType>()
            .HasData(SeedData.For(SampleData.Shows.AllTicketTypes));
        modelBuilder.Entity<TicketOrder>()
            .HasData(SeedData.For(SampleData.TicketOrders.AllTicketOrders));
        modelBuilder.Entity<TicketOrderItem>()
            .HasData(SeedData.For(SampleData.TicketOrders.AllTicketOrderItems));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Conventions.Add(_ => new StringNotUnicodeConvention());
        NodaTimeConverters.AddNodaTimeConverters(configurationBuilder);
    }
}