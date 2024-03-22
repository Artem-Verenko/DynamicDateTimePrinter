namespace DynamicDateTimePrinter;

public interface IConfigurationManager
{
    string DateFormat { get; }
    int IntervalInSeconds { get; }

    TimeSpan GetUpdateInterval();
}