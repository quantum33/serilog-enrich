using System;
using LoggingLib;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1
{
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
            _logger.LogInformation(new Payload<Person>
            {
                Data = new Person
                {
                    Name = "Demiurgo",
                    FirstName = "Yeah",
                }
            });
        }
    }
}