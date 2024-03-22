

using DynamicDateTimePrinter;

var configManager = new ConfigurationManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));
var dateTimeDisplayService = new DateTimeDisplayService(configManager);

while (true)
{
    dateTimeDisplayService.DisplayCurrentDateTime();
    await Task.Delay(configManager.GetUpdateInterval());
}
