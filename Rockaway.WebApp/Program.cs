using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Services;

namespace Rockaway.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        builder.Services.AddSingleton<IStatusReporter, StatusReporter>();
        var sqliteConnection = new SqliteConnection("Data Source=:memory:");
        sqliteConnection.Open();
        builder.Services.AddDbContext<RockawayDbContext>(options => options.UseSqlite(sqliteConnection));
   
        builder.Services.AddRazorPages();
        
        builder.Services.AddControllersWithViews();

        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        
        using (var scope = app.Services.CreateScope()) {
            using (var db = scope.ServiceProvider.GetService<RockawayDbContext>()!) {
                db.Database.EnsureCreated();
            }
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        
        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

        app.MapGet("/status", (IStatusReporter reporter) => reporter.GetStatus());
        app.MapGet("/uptime", (IStatusReporter reporter) => reporter.GetUptime());

        app.Run();
    }
}