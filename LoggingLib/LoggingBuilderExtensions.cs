using System;
using System.Runtime.Serialization.Json;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace LoggingLib
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddCustomLogger(this ILoggingBuilder builder)
            => builder.AddSerilog(logger: CreateLogger());

        private static ILogger CreateLogger() => new LoggerConfiguration()
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
            .WriteTo.Console(new LogEntryFormatter(CreateContext()))
            .CreateLogger();

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
}