using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Data.Sample;

public static class SeedData
{
    public static IEnumerable<object> For(IEnumerable<Artist> artists)
    {
        return artists.Select(ToSeedData);
    }

    public static IEnumerable<object> For(IEnumerable<Venue> venues)
    {
        return venues.Select(ToSeedData);
    }

    public static IEnumerable<object> For(IEnumerable<Show> shows)
    {
        return shows.Select(ToSeedData);
    }

    public static IEnumerable<object> For(IEnumerable<SupportSlot> supportSlots)
    {
        return supportSlots.Select(ToSeedData);
    }

    private static object ToSeedData(Artist artist)
    {
        return new
        {
            artist.Id,
            artist.Name,
            artist.Description,
            artist.Slug
        };
    }

    private static object ToSeedData(Venue venue)
    {
        return new
        {
            venue.Id,
            venue.Name,
            venue.Slug,
            venue.Address,
            venue.City,
            venue.PostalCode,
            venue.CountryCode,
            venue.Telephone,
            venue.WebsiteUrl
        };
    }

    private static object ToSeedData(Show show)
    {
        return new
        {
            VenueId = show.Venue.Id,
            show.Date,
            HeadlineArtistId = show.HeadlineArtist.Id
        };
    }

    private static object ToSeedData(SupportSlot slot)
    {
        return new
        {
            ShowVenueId = slot.Show.Venue.Id,
            ShowDate = slot.Show.Date,
            slot.SlotNumber,
            ArtistId = slot.Artist.Id
        };
    }
}