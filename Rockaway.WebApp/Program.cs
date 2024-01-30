using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Sample;
using Rockaway.WebApp.Hosting;
using Rockaway.WebApp.Services;

namespace Rockaway.WebApp;

// ReSharper disable InvokeAsExtensionMethod
public sealed class Program
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

        builder.Services.AddSingleton<IClock>(SystemClock.Instance);

        builder.Services.AddRazorPages(options => options.Conventions.AuthorizeAreaFolder("admin", "/"));

        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        builder.Services.AddControllersWithViews(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        });

        var logger = CreateAdHocLogger<Program>();

        logger.LogInformation("Rockaway running in {environment} environment", builder.Environment.EnvironmentName);

        if (HostEnvironmentExtensions.UseSqlite(builder.Environment))
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

        builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<RockawayDbContext>();

        if (HostEnvironmentEnvExtensions.IsDevelopment(builder.Environment))
            builder.Services.AddSassCompiler();

        var app = builder.Build();

        if (HostEnvironmentEnvExtensions.IsProduction(app.Environment))
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
            bool.TryParse(app.Configuration["apply-migrations"], out var applyMigrations);
            if (HostEnvironmentExtensions.UseSqlite(app.Environment) || applyMigrations)
            {
                db.Database.EnsureCreated();
                
                var adminUser = db.Users.FirstOrDefault(user => user.Id == SampleData.Users.Admin.Id);
                if (adminUser == null)
                {
                    db.Users.Add(SampleData.Users.Admin);
                    db.SaveChanges();
                }

                if (applyMigrations)
                {
                    logger.LogInformation("apply-migrations=true was specified. Applying EF migrations and then exiting.");
                    db.Database.Migrate();
                    logger.LogInformation("EF database migrations applied successfully.");
                    Environment.Exit(0);
                }
            }
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapGet("/status", (IStatusReporter reporter) => reporter.GetStatus());

        app.MapAreaControllerRoute(
            "admin",
            "Admin",
            "Admin/{controller=Home}/{action=Index}/{id?}"
        ).RequireAuthorization();

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

        app.MapControllers();

        app.Run();
    }
}