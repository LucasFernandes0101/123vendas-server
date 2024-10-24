using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Application.Configurations;

[ExcludeFromCodeCoverage]
public static class SerilogExtension
{
    private static readonly LogEventLevel _defaultLogLevel = LogEventLevel.Information;
    private static readonly LoggingLevelSwitch _loggingLevel = new();

    public static IServiceCollection AddLoggingSerilog(this IServiceCollection services, LoggerConfiguration logger)
    {
        LoadLogLevel();
        ConfigureLog(logger);

        return services;
    }

    private static void ConfigureLog(LoggerConfiguration logger)
    {
        Log.Logger = logger.Enrich.FromLogContext()
                                  .WriteTo.Console()
                                  .MinimumLevel.ControlledBy(_loggingLevel)
                                  .MinimumLevel.Verbose()
                                  .CreateLogger();
    }

    private static void LoadLogLevel()
    {
        var configLogLevel = Environment.GetEnvironmentVariable("LOG_LEVEL");

        bool parsed = Enum.TryParse(configLogLevel, true, out LogEventLevel logLevel);
        _loggingLevel.MinimumLevel = parsed ? logLevel : _defaultLogLevel;
    }
}