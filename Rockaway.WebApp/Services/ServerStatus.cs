namespace Rockaway.WebApp.Services;

public sealed class ServerStatus
{
    public string Assembly { get; set; } = string.Empty;
    public string Modified { get; set; } = string.Empty;
    public string Hostname { get; set; } = string.Empty;
    public string DateTime { get; set; } = string.Empty;
}