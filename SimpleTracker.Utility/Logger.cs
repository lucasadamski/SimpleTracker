using Serilog;
using Serilog.Events;

namespace SimpleTracker.Utility
{
    public static class Logger
    {
        public static Serilog.ILogger Log;

        static Logger()
        {
            Log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .MinimumLevel.Override("System", LogEventLevel.Verbose)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Verbose)
                .WriteTo.Console(outputTemplate: "{Timestamp:dd-MM-yy HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
                .WriteTo.File("logs/log.txt",
                              rollingInterval: RollingInterval.Day,
                              outputTemplate: "{Timestamp:dd-MM-yy HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();

            var dir = Directory.GetCurrentDirectory();
        }
    }
}

