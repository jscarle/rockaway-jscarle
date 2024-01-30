using AngleSharp;
using Shouldly;

namespace Rockaway.WebApp.Tests;

public sealed class PageTests
{
    [Theory]
    [InlineData("/")]
    [InlineData("/privacy")]
    [InlineData("/contact")]
    [InlineData("/about")]
    public async Task Page_Returns_Success(string path)
    {
        await using var factory = WebApplicationFactoryHelper.GetInstance();
        using var client = factory.CreateClient();
        using var response = await client.GetAsync(path);
        response.EnsureSuccessStatusCode();
    }

    [Theory]
    [InlineData("/", "Rockaway")]
    [InlineData("/privacy", "Privacy Policy | Rockaway")]
    [InlineData("/contact", "Contact Us | Rockaway")]
    [InlineData("/about", "About Us | Rockaway")]
    public async Task Page_Has_Correct_Title(string url, string title)
    {
        var browsingContext = BrowsingContext.New(Configuration.Default);
        await using var factory = WebApplicationFactoryHelper.GetInstance();
        var client = factory.CreateClient();
        var html = await client.GetStringAsync(url);
        var dom = await browsingContext.OpenAsync(req => req.Content(html));
        var titleTag = dom.QuerySelector("title");
        titleTag.ShouldNotBeNull();
        titleTag.InnerHtml.ShouldBe(title);
    }

    [Fact]
    public async Task ContactPage_Has_Correct_Information()
    {
        const string email = "hello@rockaway.dev";
        const string phoneNumber = "02055551234";
        const string formattedPhoneNumber = "020 5555 1234";

        var browsingContext = BrowsingContext.New(Configuration.Default);
        await using var factory = WebApplicationFactoryHelper.GetInstance();
        var client = factory.CreateClient();
        var html = await client.GetStringAsync("/contact");
        var dom = await browsingContext.OpenAsync(req => req.Content(html));

        var anchorTags = dom.QuerySelectorAll("a");
        var emailTag = anchorTags.Single(element =>
            element.Attributes.Any(attribute => attribute.Value.StartsWith("mailto:")));
        emailTag.Attributes.First(attribute => attribute.Name == "href").Value.ShouldBe($"mailto:{email}");
        emailTag.InnerHtml.ShouldBe(email);

        var telephoneTag =
            anchorTags.Single(element => element.Attributes.Any(attribute => attribute.Value.StartsWith("tel:")));
        telephoneTag.Attributes.First(attribute => attribute.Name == "href").Value.ShouldBe($"tel:{phoneNumber}");
        telephoneTag.InnerHtml.ShouldBe(formattedPhoneNumber);
    }
}