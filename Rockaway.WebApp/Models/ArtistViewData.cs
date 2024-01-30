using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Models;

public class ArtistViewData(Artist artist)
{
    private const string CloudinaryUrlTemplate =
        "https://res.cloudinary.com/ursatile/image/upload/c_fill,g_auto:face,w_{0},h_{1}/rockaway/{2}.jpg";

    public string Name { get; } = artist.Name;

    public string Slug { get; } = artist.Slug;

    public string Description { get; } = artist.Description;

    public string CssClass => Name.Length > 20 ? "long-name" : "";

    public string GetImageUrl(int width, int height)
    {
        return string.Format(CloudinaryUrlTemplate, width, height, Slug);
    }
}