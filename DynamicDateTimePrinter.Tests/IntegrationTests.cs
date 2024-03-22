
using DynamicDateTimePrinter;

[TestFixture]
public class IntegrationTests
{
    private string _configFilePath = "IntegrationTestConfig.json";
    private ConfigurationManager _configManager;
    private DateTimeDisplayService _dateTimeDisplayService;

    [SetUp]
    public void SetUp()
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;
        _configFilePath = Path.Combine(directory, "IntegrationTestConfig.json");
        var initialConfig = @"{""DateFormat"":""yyyy-MM-dd HH:mm:ss"", ""IntervalInSeconds"":2}";
        File.WriteAllText(_configFilePath, initialConfig);

        _configManager = new ConfigurationManager(_configFilePath);
        _dateTimeDisplayService = new DateTimeDisplayService(_configManager);
    }

    [Test]
    public void ConfigurationManagerAndDateTimeDisplayService_WhenFileChanges_ShouldReflectNewSettings()
    {
        // Waiting for the configuration to update
        Thread.Sleep(1100);

        // Changing the configuration
        var newConfig = @"{""DateFormat"":""MM/dd/yyyy HH:mm"", ""IntervalInSeconds"":5}";
        File.WriteAllText(_configFilePath, newConfig);

        // Waiting for the configuration to update
        Thread.Sleep(1100);

        // Verify that the configuration has been updated
        Assert.AreEqual("MM/dd/yyyy HH:mm", _configManager.DateFormat);
        Assert.AreEqual(5, _configManager.IntervalInSeconds);

        // Verify that DateTimeDisplayService is using the new date format
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            _dateTimeDisplayService.DisplayCurrentDateTime();
            Assert.IsTrue(sw.ToString().Contains(DateTime.Now.ToString("MM/dd/yyyy HH:mm")));
        }
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_configFilePath))
        {
            File.Delete(_configFilePath);
        }
    }
}
