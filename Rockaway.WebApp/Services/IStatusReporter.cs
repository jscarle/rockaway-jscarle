namespace Rockaway.WebApp.Services;

public interface IStatusReporter
{
    public ServerStatus GetStatus();

    public int GetUptime();
}