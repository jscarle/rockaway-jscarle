using System.Globalization;
using NodaTime;

namespace Rockaway.WebApp.Data.Entities;

public sealed class Show
{
    public Venue Venue { get; set; } = default!;

    public LocalDate Date { get; set; }

    public Artist HeadlineArtist { get; set; } = default!;

    public List<SupportSlot> SupportSlots { get; set; } = [];

    public int NextSupportSlotNumber
        => (SupportSlots.Count > 0 ? SupportSlots.Max(s => s.SlotNumber) : 0) + 1;

    public List<TicketType> TicketTypes { get; set; } = [];

    public Dictionary<string, string> RouteData => new()
    {
        { "venue", Venue.Slug },
        { "date", Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) }
    };
}