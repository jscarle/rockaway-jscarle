using System.ComponentModel.DataAnnotations;

namespace Rockaway.WebApp.Data.Entities;

public class TicketType(Guid id, Show show, string name, decimal price, int? limit = null)
{
    // Private constructor required by EF Core
    // ReSharper disable once UnusedMember.Local
    private TicketType() : this(Guid.Empty, default!, default!, default)
    {
    }

    public Guid Id { get; set; } = id;
    public Show Show { get; set; } = show;
    [MaxLength(500)] public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
    public int? Limit { get; set; } = limit;

    public string FormattedPrice
        => Show.Venue.FormatPrice(Price);

    public string FormatPrice(int quantity)
    {
        return Show.Venue.FormatPrice(Price * quantity);
    }
}