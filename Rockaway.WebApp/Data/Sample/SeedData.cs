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

    public static IEnumerable<object> For(IEnumerable<TicketType> ticketTypes)
    {
        return ticketTypes.Select(ToSeedData);
    }

    public static IEnumerable<object> For(IEnumerable<TicketOrder> ticketOrders)
    {
        return ticketOrders.Select(o => new
        {
            o.Id,
            o.CustomerName,
            o.CustomerEmail,
            o.CreatedAt,
            o.CompletedAt,
            ShowDate = o.Show.Date,
            ShowVenueId = o.Show.Venue.Id
        });
    }

    public static IEnumerable<object> For(IEnumerable<TicketOrderItem> ticketOrderItems)
    {
        return ticketOrderItems.Select(item => new
        {
            TicketOrderId = item.TicketOrder.Id,
            TicketTypeId = item.TicketType.Id,
            item.Quantity
        });
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
            venue.CultureName,
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

    private static object ToSeedData(TicketType tt)
    {
        return new
        {
            tt.Id,
            ShowVenueId = tt.Show.Venue.Id,
            ShowDate = tt.Show.Date,
            tt.Price,
            tt.Name
        };
    }
}