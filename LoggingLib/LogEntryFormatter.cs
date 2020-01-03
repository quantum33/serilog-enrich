using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace LoggingLib
{
    public class LogEntryFormatter : ITextFormatter
    {
        // public LogEntryFormatter(Context context)
        // {
        //     _context = context ?? throw new ArgumentNullException(nameof(context));
        // }

        // public void Format(LogEvent logEvent, TextWriter output)
        // {
        //     var serializeContext = GetSerializeContext();
        //
        //     LogEventPropertyValue payload = logEvent.Properties["_payload"];
        //     var contextProperty = logEvent.Properties["_context"];
        //     
        //     var formatter = new JsonValueFormatter(typeTagName: null);
        //
        //     output.Write($"{{\"context\": {serializeContext}, \"payload\": ");
        //     formatter.Format(payload, output);
        //     output.Write("}");
        // }
        
        public void Format(LogEvent logEvent, TextWriter output)
        {
            LogEventPropertyValue payload = logEvent.Properties["_payload"];
            var contextProperty = logEvent.Properties["_context"];
            
            var formatter = new JsonValueFormatter(typeTagName: null);

            output.Write($"{{\"context\": ");
            formatter.Format(contextProperty, output);
            output.Write(", \"payload\": ");
            formatter.Format(payload, output);
            output.Write("}");
        }

        // private string GetSerializeContext()
        // {
        //     if (string.IsNullOrEmpty(_serializeContext))
        //     {
        //         _serializeContext = JsonConvert.SerializeObject(
        //             _context,
        //             new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
        //     }
        //
        //     return _serializeContext;
        // }

        // private readonly Context _context;
        //private string _serializeContext;
    }
}