namespace Rockaway.WebApp.Services;

public sealed class ServerStatus
{
    public string Assembly { get; init; } = string.Empty;
    public string Modified { get; init; } = string.Empty;
    public string Hostname { get; init; } = string.Empty;
    public string DateTime { get; init; } = string.Empty;
    public string Uptime { get; init; } = string.Empty;
}