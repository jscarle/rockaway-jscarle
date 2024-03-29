﻿using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace Rockaway.WebApp.Tests.Areas.Admin;

public sealed class SecurityTests
{
    [Fact]
    public async Task Admin_Returns_Redirect_When_Not_Signed_In()
    {
        await using var factory = WebApplicationFactoryHelper.GetInstance();
        var doNotFollowRedirects = new WebApplicationFactoryClientOptions { AllowAutoRedirect = false };
        using var client = factory.CreateClient(doNotFollowRedirects);
        using var response = await client.GetAsync("/admin");
        response.StatusCode.ShouldBe(HttpStatusCode.Found);
        response.Headers.Location?.ToString().ShouldContain("/identity/account/login");
    }
}