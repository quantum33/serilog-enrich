using System;
using System.Runtime.Serialization.Json;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Configuration;
using ILogger = Serilog.ILogger;

namespace LoggingLib
{
    public static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddCustomLogger(this ILoggingBuilder builder, IConfiguration configuration)
            => builder.AddSerilog(logger: CreateLogger(configuration));

        // private static ILogger CreateLoggerWithoutConfigurationFile()
        // {
        //     var logger = new LoggerConfiguration()
        //         .Enrich.FromLogContext()
        //         .Enrich.With(new ContextEnricher())
        //         // .Destructure.ByTransforming<Context>(
        //         //     c => new
        //         //     {
        //         //         User = c.UserId,
        //         //         TraceId = c.TraceId,
        //         //         ApplicationName = c.Application.Name,
        //         //         ApplicationVersion = c.Application.Version
        //         //     })
        //         .WriteTo.Console(new LogEntryFormatter( /*CreateContext()*/))
        //         .CreateLogger();
        //
        //     return logger;
        // }

        private static ILogger CreateLogger(IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return logger;
        }
        
        /// <summary>
        /// This method is used by configuration file.
        /// * see the "using" section with specified assembly (eg: LoggingLib)
        /// * "Enrich" section reference this method <see cref="WithContextEnricher"/>
        /// </summary>
        /// <param name="enrichmentConfiguration">the enrichment configuration</param>
        /// <returns>the enrichment configuration itself</returns>
        public static LoggerConfiguration WithContextEnricher(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            return enrichmentConfiguration.With<ContextEnricher>();
        }
        
        // private static Context CreateContext() => new Context
        // {
        //     Application = new Application
        //     {
        //         Name = "ConsoleApp 1",
        //         Version = "0.1"
        //     },
        //     TraceId = Guid.NewGuid().ToString(),
        //     UserId = "user_459123"
        // };
    }
}