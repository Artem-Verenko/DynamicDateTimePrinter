using DynamicDateTimePrinter;

[TestFixture]
public class ConfigurationManagerTests
{
    private string _configFilePath = "IntegrationTestConfig.json";
    private ConfigurationManager _configManager;

    [SetUp]
    public void SetUp()
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;
        _configFilePath = Path.Combine(directory, "IntegrationTestConfig.json");
        var configContent = @"{""DateFormat"":""yyyy-MM-dd HH:mm:ss"", ""IntervalInSeconds"":10}";
        File.WriteAllText(_configFilePath, configContent);
        _configManager = new ConfigurationManager(_configFilePath);
    }

    [Test]
    public void LoadConfiguration_ValidConfiguration_ShouldLoadCorrectly()
    {
        Assert.AreEqual("yyyy-MM-dd HH:mm:ss", _configManager.DateFormat);
        Assert.AreEqual(10, _configManager.IntervalInSeconds);
    }

    [Test]
    public void LoadConfiguration_InvalidJsonFormat_ShouldHandleErrorAndSetDefaults()
    {
        // Write clearly invalid JSON to the file
        File.WriteAllText(_configFilePath, "INVALID JSON");

        // Try reloading the configuration
        _configManager = new ConfigurationManager(_configFilePath);

        // Check that default values are set
        Assert.AreEqual("yyyy-MM-dd HH:mm:ss", _configManager.DateFormat);
        Assert.AreEqual(10, _configManager.IntervalInSeconds);
    }


    [Test]
    public void LoadConfiguration_FileChanged_ShouldReloadConfiguration()
    {
       
        SetUp();

        var newConfigContent = @"{""DateFormat"":""MM-dd-yyyy"", ""IntervalInSeconds"":20}";
        File.WriteAllText(_configFilePath, newConfigContent);

        Thread.Sleep(1000); 

        Assert.AreEqual("MM-dd-yyyy", _configManager.DateFormat);
        Assert.AreEqual(20, _configManager.IntervalInSeconds);
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