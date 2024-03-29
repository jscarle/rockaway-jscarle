using Microsoft.AspNetCore.Mvc.RazorPages;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Pages;

public sealed class VenuesModel(RockawayDbContext db) : PageModel
{
    public IEnumerable<Venue> Venues = default!;

    public void OnGet()
    {
        Venues = db.Venues.ToList();
    }
}