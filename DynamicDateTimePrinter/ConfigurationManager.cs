
using Newtonsoft.Json;

namespace DynamicDateTimePrinter
{
    public class ConfigurationManager : IConfigurationManager
    {
        public string DateFormat { get; private set; } = "yyyy-MM-dd HH:mm:ss";
        public int IntervalInSeconds { get; private set; } = 10;
        private readonly string _filePath;
        private FileSystemWatcher _fileWatcher;

        public ConfigurationManager(string filePath)
        {
            _filePath = filePath;
            LoadConfiguration();
            InitializeWatcher();
        }

        private void InitializeWatcher()
        {
            _fileWatcher = new FileSystemWatcher(Path.GetDirectoryName(_filePath))
            {
                Filter = Path.GetFileName(_filePath),
                NotifyFilter = NotifyFilters.LastWrite
            };
            _fileWatcher.Changed += (s, e) => LoadConfiguration();
            _fileWatcher.EnableRaisingEvents = true;
        }

        private void LoadConfiguration()
        {
            const int maxAttempts = 5;
            int attempts = 0;
            bool success = false;

            while (attempts < maxAttempts && !success)
            {
                try
                {
                    using (var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (var reader = new StreamReader(stream))
                    {
                        var json = reader.ReadToEnd();
                        var config = JsonConvert.DeserializeObject<Configuration>(json);
                        if (config != null)
                        {
                            DateFormat = config.DateFormat;
                            IntervalInSeconds = config.IntervalInSeconds;
                            success = true;
                        }
                    }
                }
                catch (IOException)
                {
                    attempts++;
                    if (attempts >= maxAttempts)
                    {
                        Console.WriteLine("Error loading configuration: Unable to access the config file after several attempts.");
                    }
                    Task.Delay(500).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading configuration: {ex.Message}");
                    break;
                }
                if (!success) // If loading was not successful, revert to default values
                {
                    DateFormat = "yyyy-MM-dd HH:mm:ss";
                    IntervalInSeconds = 10;
                }
            }
        }

        public TimeSpan GetUpdateInterval() => TimeSpan.FromSeconds(IntervalInSeconds);

        private sealed record Configuration(string DateFormat, int IntervalInSeconds);
    }
}