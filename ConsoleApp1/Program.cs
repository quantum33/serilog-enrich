using System;
using LoggingLib;
using Microsoft.Extensions.DependencyInjection;
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
                .AddLogging(builder => builder.AddCustomLogger())
                .AddSingleton<SampleApplication>();

            var serviceProvider = collection.BuildServiceProvider();

            var sampleApplication = serviceProvider.GetService<SampleApplication>();

            sampleApplication.Run();

            serviceProvider.Dispose();
        }
    }
}