using System.Reflection;

namespace Rockaway.WebApp.Services;

public sealed class StatusReporter : IStatusReporter
{
    private static readonly Assembly Assembly = Assembly.GetEntryAssembly()!;

    public ServerStatus GetStatus()
    {
        return new ServerStatus
        {
            Assembly = Assembly.FullName ?? "Assembly.GetEntryAssembly() returned null",
            Modified = new DateTimeOffset(File.GetLastWriteTimeUtc(Assembly.Location), TimeSpan.Zero).ToString("O"),
            Hostname = Environment.MachineName,
            DateTime = DateTimeOffset.UtcNow.ToString("O")
        };
    }
}