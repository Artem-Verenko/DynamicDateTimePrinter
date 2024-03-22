using System;

namespace DynamicDateTimePrinter
{
    public class DateTimeDisplayService : IDateTimeDisplayService
    {
        private readonly IConfigurationManager _configManager;

        public DateTimeDisplayService(IConfigurationManager configManager)
        {
            _configManager = configManager;
        }

        public void DisplayCurrentDateTime()
        {
            var now = DateTime.Now;
            var format = _configManager.DateFormat;
            Console.WriteLine(now.ToString(format));
        }
    }
}