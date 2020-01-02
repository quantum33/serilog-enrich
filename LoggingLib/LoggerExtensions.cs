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
            //string serialized = JsonSerializer.Serialize(payload.Data);
            // var serialized = JsonConvert.SerializeObject(
            //     payload.Data,
            //     new JsonSerializerSettings
            //     {
            //         ContractResolver = new DefaultContractResolver
            //         {
            //             NamingStrategy = new CamelCaseNamingStrategy()
            //         }
            //     });

            using (LogContext.PushProperty("_payload", payload.Data, destructureObjects: true))
            {
                //logger.LogInformation(new EventId(), "{_context} {_payload}");
                logger.LogInformation("ffff");
            }
        }
    }
}