using Blazored.LocalStorage;
using Dashboard.Components;
using Dashboard.Extensions;
using Dashboard.Preferences;
using FlowSynx.Client;
using FlowSynx.Environment;
using FlowSynx.IO;
using MudBlazor.Services;

namespace Dashboard;

public static class Program
{
    public static async Task Main(string[] args)
    {
        WebApplication? app = null;

        try
        {
            var builder = WebApplication.CreateBuilder();

            builder.WebHost.ConfigHttpServer(EnvironmentVariables.FlowSynxDashboardHttpPort);

            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var cliArguments = configuration.BindCliArguments();

            builder.Services
                .AddMudServices()
                .AddBlazoredLocalStorage()
                .AddLoggingService()
                .AddSerialization()
                .AddScoped<IPreferenceManager, PreferenceManager>()
                .AddScoped<IFlowSynxClient, FlowSynxClient>()
                .AddSingleton(new FlowSynxClientConnection { BaseAddress = cliArguments.Address });

            app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            if (app != null)
            {
                var logger = app.Services.GetRequiredService<ILogger>();
                logger.Log(LogLevel.Error, ex.Message);
            }
            else
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}