using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace Rockaway.WebApp.Data.Entities;

public class TicketOrder
{
    public Guid Id { get; set; }
    public Show Show { get; set; } = default!;
    public List<TicketOrderItem> Contents { get; set; } = [];
    [MaxLength(500)] public string CustomerName { get; set; } = string.Empty;
    [MaxLength(500)] public string CustomerEmail { get; set; } = string.Empty;
    public Instant CreatedAt { get; set; }
    public Instant? CompletedAt { get; set; }

    public string FormattedTotalPrice
        => Show.Venue.FormatPrice(Contents.Sum(item => item.TicketType.Price * item.Quantity));

    public string Reference => Id.ToString("D")[..8].ToUpperInvariant();

    public TicketOrderItem UpdateQuantity(TicketType ticketType, int quantity)
    {
        var item = Contents.FirstOrDefault(toi => toi.TicketType == ticketType);
        if (item == default)
        {
            item = new TicketOrderItem { TicketOrder = this, TicketType = ticketType };
            Contents.Add(item);
        }

        item.Quantity = quantity;
        return item;
    }
}