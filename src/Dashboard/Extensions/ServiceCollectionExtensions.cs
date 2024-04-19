using FlowSynx.Logging;

namespace Dashboard.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLoggingService(this IServiceCollection services)
    {
        services.AddLogging(c => c.ClearProviders());
        const string template = "[time={timestamp} | level={level} | machine={machine}] message=\"{message}\"";
        services.AddLogging(builder => builder.AddConsoleLogger(config =>
        {
            config.OutputTemplate = template;
            config.MinLevel = LogLevel.Information;
        }));
        return services;
    }
}