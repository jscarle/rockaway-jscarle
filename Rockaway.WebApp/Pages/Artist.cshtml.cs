using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Models;

namespace Rockaway.WebApp.Pages;

public class ArtistModel(RockawayDbContext db) : PageModel
{
    public ArtistViewData Artist = default!;

    public IActionResult OnGet(string slug)
    {
        var artist = db.Artists
            .Include(a => a.HeadlineShows)
            .ThenInclude(show => show.Venue)
            .Include(a => a.HeadlineShows)
            .ThenInclude(show => show.SupportSlots)
            .ThenInclude(slot => slot.Artist)
            .FirstOrDefault(a => a.Slug == slug);
        if (artist == default)
            return NotFound();
        Artist = new ArtistViewData(artist);
        return Page();
    }
}