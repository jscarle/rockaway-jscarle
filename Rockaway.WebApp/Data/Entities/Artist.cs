using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Rockaway.WebApp.Data.Entities;

public class Artist
{
    public Artist()
    {
    }

    public Artist(Guid id, string name, string description, string slug)
    {
        Id = id;
        Name = name;
        Description = description;
        Slug = slug;
    }

    public Guid Id { get; set; }

    [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [MaxLength(500)] public string Description { get; set; } = string.Empty;

    [MaxLength(100)] [Unicode(false)] public string Slug { get; set; } = string.Empty;
}