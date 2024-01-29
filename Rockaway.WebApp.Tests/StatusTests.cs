using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Rockaway.WebApp.Services;
using Shouldly;

namespace Rockaway.WebApp.Tests;

public sealed class StatusTests
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

    private static readonly ServerStatus TestStatus = new()
    {
        Assembly = "TEST_ASSEMBLY",
        Modified = new DateTimeOffset(2021, 2, 3, 4, 5, 6, TimeSpan.Zero).ToString("O"),
        Hostname = "TEST_HOST",
        DateTime = new DateTimeOffset(2022, 3, 4, 5, 6, 7, TimeSpan.Zero).ToString("O"),
        Uptime = "394:1:01:01"
    };

    [Fact]
    public async Task Status_Endpoint_Returns_Status()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStatusReporter>(new TestStatusReporter());
            }));
        var client = factory.CreateClient();
        var json = await client.GetStringAsync("/status");
        var status = JsonSerializer.Deserialize<ServerStatus>(json, JsonSerializerOptions);
        status.ShouldNotBeNull();
        status.ShouldBeEquivalentTo(TestStatus);
    }

    [Fact]
    public async Task Uptime_Endpoint_Returns_Uptime()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStatusReporter>(new TestStatusReporter());
            }));
        var client = factory.CreateClient();
        var uptime = await client.GetStringAsync("/uptime");
        uptime.ShouldNotBeNull();
        uptime.ShouldBe("34045261");
    }
    private class TestStatusReporter : IStatusReporter
    {
        public ServerStatus GetStatus()
        {
            return TestStatus;
        }

        public int GetUptime()
        {
            return 34045261;
        }
    }
}