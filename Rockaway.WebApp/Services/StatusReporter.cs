using System.Reflection;

namespace Rockaway.WebApp.Services;

public sealed class StatusReporter : IStatusReporter
{
    private static readonly Assembly Assembly = Assembly.GetEntryAssembly()!;

    private static readonly DateTimeOffset Startup = DateTimeOffset.UtcNow;
    private static readonly DateTimeOffset LastModified = new(File.GetLastWriteTimeUtc(Assembly.Location), TimeSpan.Zero);

    public ServerStatus GetStatus()
    {
        return new ServerStatus
        {
            Assembly = Assembly.FullName ?? "Assembly.GetEntryAssembly() returned null",
            Modified = LastModified.ToString("O"),
            Hostname = Environment.MachineName,
            DateTime = DateTimeOffset.UtcNow.ToString("O"),
            Uptime = DateTimeOffset.UtcNow.Subtract(Startup.UtcDateTime).ToString("g").Split('.')[0]
        };
    }
}