using System.Text.Json;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace LoggingLib
{
    public static class LoggerExtensions
    {
        public static void LogInformation<T>(this ILogger logger, Payload<T> payload)
        {
            string serialized = JsonSerializer.Serialize(payload.Data);
            using (LogContext.PushProperty("_payload", serialized))
            {
                logger.LogInformation(new EventId(), "{_context} {_payload}");
            }
        }
    }
}