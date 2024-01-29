using Rockaway.WebApp.Services;

namespace Rockaway.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddSingleton<IStatusReporter, StatusReporter>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapGet("/status", (IStatusReporter reporter) => reporter.GetStatus());
        app.MapGet("/uptime", (IStatusReporter reporter) => reporter.GetUptime());

        app.Run();
    }
}