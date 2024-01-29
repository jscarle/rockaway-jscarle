using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Hosting;
using Rockaway.WebApp.Services;

namespace Rockaway.WebApp;

public class Program
{
    private static ILogger<T> CreateAdHocLogger<T>()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables()
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
            .Build();
        return LoggerFactory.Create(lb => lb.AddConfiguration(config)).CreateLogger<T>();
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IStatusReporter, StatusReporter>();

        builder.Services.AddRazorPages();

        builder.Services.AddControllersWithViews();

        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        var logger = CreateAdHocLogger<Program>();

        logger.LogInformation("Rockaway running in {environment} environment", builder.Environment.EnvironmentName);

        if (builder.Environment.UseSqlite())
        {
            logger.LogInformation("Using Sqlite database");
            var sqliteConnection = new SqliteConnection("Data Source=:memory:");
            sqliteConnection.Open();
            builder.Services.AddDbContext<RockawayDbContext>(options => options.UseSqlite(sqliteConnection));
        }
        else
        {
            logger.LogInformation("Using SQL Server database");
            var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            builder.Services.AddDbContext<RockawayDbContext>(options => options.UseSqlServer(connectionString));
        }

        var app = builder.Build();

        if (app.Environment.IsProduction())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }

        using (var scope = app.Services.CreateScope())
        {
            using var db = scope.ServiceProvider.GetService<RockawayDbContext>()!;
            if (app.Environment.UseSqlite())
            {
                db.Database.EnsureCreated();
            }
            else if (bool.TryParse(app.Configuration["apply-migrations"], out var applyMigrations) && applyMigrations)
            {
                logger.LogInformation("apply-migrations=true was specified. Applying EF migrations and then exiting.");
                db.Database.Migrate();
                logger.LogInformation("EF database migrations applied successfully.");
                Environment.Exit(0);
            }
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

        app.MapGet("/status", (IStatusReporter reporter) => reporter.GetStatus());

        app.Run();
    }
}