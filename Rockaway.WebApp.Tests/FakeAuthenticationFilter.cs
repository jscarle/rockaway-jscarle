using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Rockaway.WebApp.Tests;

internal class FakeAuthenticationFilter(string emailAddress) : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return builder =>
        {
            var options = FakeAuthenticationOptions.Create(emailAddress);
            builder.UseMiddleware<FakeAuthenticationMiddleware>(options);
            next(builder);
        };
    }

    private class FakeAuthenticationOptions(string emailAddress)
    {
        public string EmailAddress { get; } = emailAddress;

        internal static IOptions<FakeAuthenticationOptions> Create(string emailAddress)
        {
            return Options.Create(new FakeAuthenticationOptions(emailAddress));
        }
    }

    private class FakeAuthenticationMiddleware(RequestDelegate next, IOptions<FakeAuthenticationOptions> options)
    {
        private readonly string _authenticationType = IdentityConstants.ApplicationScheme;
        private readonly string _emailAddress = options.Value.EmailAddress;

        public async Task InvokeAsync(HttpContext context)
        {
            var claims = new Claim[]
            {
                new(ClaimTypes.Name, _emailAddress),
                new(ClaimTypes.Email, _emailAddress)
            };
            var identity = new ClaimsIdentity(claims, _authenticationType);
            context.User = new ClaimsPrincipal(identity);
            await next(context);
        }
    }
}