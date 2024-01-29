namespace Rockaway.WebApp.Hosting;

public static class HostEnvironmentExtensions
{
    private static readonly string[] SqliteEnvironments = ["UnitTest", Environments.Development];

    public static bool UseSqlite(this IHostEnvironment env)
    {
        return SqliteEnvironments.Contains(env.EnvironmentName);
    }
}