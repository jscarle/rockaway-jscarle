using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Rockaway.WebApp.Data.Entities;

public sealed class Venue
{
    public Venue()
    {
    }

    public Venue(Guid id, string name, string slug, string address, string city, string cultureName, string? postalCode,
        string? telephone,
        string? websiteUrl)
    {
        Id = id;
        Name = name;
        Slug = slug;
        Address = address;
        City = city;
        CultureName = cultureName;
        PostalCode = postalCode;
        Telephone = telephone;
        WebsiteUrl = websiteUrl;
    }

    public List<Show> Shows { get; set; } = [];

    public Guid Id { get; set; }

    [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    [Unicode(false)]
    [RegularExpression("^[a-z0-9-]{2,100}$",
        ErrorMessage = "Slug must be 2-100 characters and can only contain a-z, 0-9 and the hyphen - character")]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(500)] public string Address { get; set; } = string.Empty;

    [MaxLength(500)] public string City { get; set; } = string.Empty;

    [Unicode(false)] [MaxLength(16)] public string CultureName { get; set; } = string.Empty;

    public string CountryCode => CultureName.Split("-").Last();

    [MaxLength(500)] public string? PostalCode { get; set; }

    [MaxLength(500)] [Phone] public string? Telephone { get; set; }

    [MaxLength(4096)] [Url] public string? WebsiteUrl { get; set; }

    public Show BookShow(Artist artist, LocalDate date)
    {
        var show = new Show
        {
            Venue = this,
            HeadlineArtist = artist,
            Date = date
        };
        Shows.Add(show);
        return show;
    }
}