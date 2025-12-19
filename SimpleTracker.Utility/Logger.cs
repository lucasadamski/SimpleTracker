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
                    .AddFilter("Microsoft", LogLevel.Trace)
                    .AddFilter("System", LogLevel.Trace)
                    .AddFilter("SimpleTracker", LogLevel.Trace)
                    .AddSimpleConsole(options =>
                    {
                        options.SingleLine = true;
                        options.TimestampFormat = "dd-MM-yy hh:mm:ss.fff ";
                    });
            });
            Log = loggerFactory.CreateLogger("SimpleTracker");
        }
    }
}
