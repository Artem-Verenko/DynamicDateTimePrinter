using DynamicDateTimePrinter;
using Moq;

[TestFixture]
public class DateTimeDisplayServiceTests
{
    private Mock<IConfigurationManager> _mockConfigManager;
    private DateTimeDisplayService _dateTimeDisplayService;
    private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

    [SetUp]
    public void SetUp()
    {
        _mockConfigManager = new Mock<IConfigurationManager>();
        _mockConfigManager.Setup(m => m.DateFormat).Returns(DateFormat);
        _dateTimeDisplayService = new DateTimeDisplayService(_mockConfigManager.Object);
    }

    [Test]
    public void DisplayCurrentDateTime_ShouldUseDateFormatFromConfiguration()
    {
        var expectedDateTimeFormat = DateTime.Now.ToString(DateFormat);

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            _dateTimeDisplayService.DisplayCurrentDateTime();

            Assert.That(sw.ToString().Trim(), Is.EqualTo(expectedDateTimeFormat));
        }
    }

    [Test]
    public void DisplayCurrentDateTime_WithDifferentDateFormat_ShouldUseNewFormat()
    {
        var newDateFormat = "MM/dd/yyyy HH:mm";
        _mockConfigManager.Setup(m => m.DateFormat).Returns(newDateFormat);
        var expectedDateTimeFormat = DateTime.Now.ToString(newDateFormat);

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            _dateTimeDisplayService.DisplayCurrentDateTime();

            Assert.That(sw.ToString().Trim(), Is.EqualTo(expectedDateTimeFormat));
        }
    }
}