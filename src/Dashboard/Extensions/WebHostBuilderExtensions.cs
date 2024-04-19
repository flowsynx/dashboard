using EnsureThat;
using Microsoft.Extensions.Configuration;

namespace Dashboard.Extensions;

public static class WebHostBuilderExtensions
{
    public static IWebHostBuilder ConfigHttpServer(this IWebHostBuilder webHost, int port)
    {
        EnsureArg.IsNotNull(webHost, nameof(webHost));
        webHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(port);
        });
        webHost.UseKestrel(option => option.AddServerHeader = false);
        return webHost;
    }
}