using Microsoft.AspNetCore.Mvc.Testing;

namespace Rockaway.WebApp.Tests;

public static class WebApplicationFactoryHelper
{
    public static WebApplicationFactory<Program> GetInstance()
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => {
                builder.ConfigureServices(services => {
                    var sassCompiler = services.FirstOrDefault(s => 
                        (s.ImplementationType?.FullName ?? string.Empty).Contains("SassCompilerHostedService"));
                    if (sassCompiler != default)
                        services.Remove(sassCompiler);
                });
            });
    }
}