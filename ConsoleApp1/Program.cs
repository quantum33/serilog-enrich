using System;
using LoggingLib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var collection = new ServiceCollection();
            collection
                .AddLogging(builder => builder.AddCustomLogger(configuration))
                .AddSingleton<SampleApplication>();

            ServiceProvider serviceProvider = collection.BuildServiceProvider();

            SampleApplication sampleApplication = serviceProvider.GetService<SampleApplication>();

            sampleApplication.Run();

            serviceProvider.Dispose();
        }
    }
}