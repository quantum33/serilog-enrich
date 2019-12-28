using System;
using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Text.Json.Serialization;

namespace LoggingLib
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomLogger(this IServiceCollection services)
        {
            var context = CreateContext();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                // .Enrich.With(new ContextEnricher(context))
                // .Destructure.ByTransforming<Context>(
                //     c => new
                //     {
                //         User = c.UserId,
                //         TraceId = c.TraceId,
                //         ApplicationName = c.Application.Name,
                //         ApplicationVersion = c.Application.Version
                //     })
                .WriteTo.Console(new LogEntryFormatter(context))
                .CreateLogger();

            return services;
        }

        private static Context CreateContext() => new Context
        {
            Application = new Application
            {
                Name = "ConsoleApp 1",
                Version = "0.1"
            },
            TraceId = Guid.NewGuid().ToString(),
            UserId = "user_459123"
        };
    }

    public class LogEntryFormatter : ITextFormatter
    {
        public LogEntryFormatter(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            LogEventPropertyValue payload = logEvent.Properties["_payload"];

            output.Write("{ \"context\":");
            output.Write(JsonConvert.SerializeObject(
                _context,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
            output.Write(", \"payload\":");
            output.Write(payload);
            output.Write("}");
        }

        private readonly Context _context;
    }
}