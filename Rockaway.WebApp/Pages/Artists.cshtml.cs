using Microsoft.AspNetCore.Mvc.RazorPages;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Models;

namespace Rockaway.WebApp.Pages;

public sealed class ArtistsModel(RockawayDbContext db) : PageModel
{
    public IEnumerable<ArtistViewData> Artists = default!;

    public void OnGet()
    {
        Artists = db.Artists.Select(artist => new ArtistViewData(artist));
    }
}