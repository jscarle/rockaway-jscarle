﻿using System.Text.Json;
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
        Uptime = "3:15:30"
    };

    [Fact]
    public async Task Status_Endpoint_Returns_Status()
    {
        await using var factory = WebApplicationFactoryHelper.GetInstance()
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

    private class TestStatusReporter : IStatusReporter
    {
        public ServerStatus GetStatus()
        {
            return TestStatus;
        }
    }
}