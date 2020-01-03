using System.Text.Json;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog.Context;

namespace LoggingLib
{
    public static class LoggerExtensions
    {
        public static void LogInformation<T>(this ILogger logger, Payload<T> payload)
        {
            using (LogContext.PushProperty("_payload", payload.Data, destructureObjects: true))
            {
                logger.Log<object>(LogLevel.Information, new EventId(), null, null, null);
            }
        }
        
        //TODO: add other methods (debug, etc.)
    }
}