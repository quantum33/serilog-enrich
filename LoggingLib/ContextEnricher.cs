using System;
using Serilog.Core;
using Serilog.Events;

namespace LoggingLib
{
    public class ContextEnricher : ILogEventEnricher
    {
        public ContextEnricher(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("_context", _context, destructureObjects: true));
        }

        private readonly Context _context;
    }
}