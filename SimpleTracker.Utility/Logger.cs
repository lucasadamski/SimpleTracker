using Microsoft.Extensions.Logging;

namespace SimpleTracker.Utility
{
    public static class Logger
    {
        public static ILogger Log;
        static Logger()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
            Log = loggerFactory.CreateLogger<int>();
        }
    }
}
