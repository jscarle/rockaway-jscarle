using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Rockaway.WebApp.Data;

public sealed class CountryFlagTagHelper : TagHelper
{
    public string IsoCode { get; set; } = default!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "img";
        output.TagMode = TagMode.SelfClosing;
        output.Attributes.Add("class", "country-flag");
        var info = new FlagInfo(IsoCode);

        output.Attributes.Add("src", info.Src);
        output.Attributes.Add("title", info.Title);
        output.Attributes.Add("alt", info.Alt);
        output.Attributes.Add("srcset", info.SrcSet);
    }

    private class FlagInfo
    {
        private readonly string _src;

        public FlagInfo(string code)
        {
            var country = Country.FromCode(code);
            _src = (country?.Code ?? "unknown").ToLowerInvariant();
            Title = country?.Name ?? $"unknown country {code}";
        }

        public string Src => $"/img/flags/1x/{_src}.png";
        public string SrcSet => $"/img/flags/2x/{_src}.png 2x,/img/flags/3x/{_src}.png 3x";
        public string Alt => $"Flag of {Title}";
        public string Title { get; }
    }
}