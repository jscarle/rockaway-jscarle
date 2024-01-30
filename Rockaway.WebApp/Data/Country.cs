namespace Rockaway.WebApp.Data;

public partial class Country
{
    private Country(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public string Code { get; set; }
    public string Name { get; set; }

    public static IList<Country> Iso3166List => Iso3166;

    public static string GetName(string code)
    {
        return FromCode(code)?.Name ?? string.Empty;
    }

    public static Country? FromCode(string countryCode)
    {
        return Iso3166List.FirstOrDefault(c => c.Code.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase));
    }
}