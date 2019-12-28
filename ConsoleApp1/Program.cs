using System;
using LoggingLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var collection = new ServiceCollection();
            collection
                .AddLogging(builder => builder.AddSerilog())
                .AddCustomLogger()
                .AddSingleton<SampleApplication>();

            var serviceProvider = collection.BuildServiceProvider();

            var sampleApplication = serviceProvider.GetService<SampleApplication>();

            sampleApplication.Run();

            serviceProvider.Dispose();
        }
    }

    public class SampleApplication
    {
        private readonly ILogger<SampleApplication> _logger;

        public SampleApplication(ILogger<SampleApplication> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Run()
        {
            Console.WriteLine("Hello SAMPLE");
            _logger.LogInformation<Person>(new Payload<Person>
            {
                Data = new Person
                {
                    Name = "Demiurgo",
                    FirstName = "Thierry",
                }
            });
        }
    }
}