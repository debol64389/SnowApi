using NUnit.Framework;
using SnowApi.Services;

namespace SnowApi.UnitTests.Services;

[TestFixture]
public class MessageTemplatesValidationServiceTests
{
    private MessageTemplatesValidationService _messageTemplatesValidationService;

    [SetUp]
    public void SetUp()
    {
        _messageTemplatesValidationService = new MessageTemplatesValidationService();
    }

    [Test]
    public void IsValidSubject_ValidSubject_ReturnsTrue()
    {
        // Arrange
        const string validSubject = "Regular Subject";

        // Act
        var result = _messageTemplatesValidationService.IsValidSubject(validSubject);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValidSubject_SubjectTooShort_ReturnsFalse()
    {
        // Arrange
        const string shortSubject = "A";

        // Act
        var result = _messageTemplatesValidationService.IsValidSubject(shortSubject);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValidSubject_SubjectTooLong_ReturnsFalse()
    {
        // Arrange
        var longSubject = new string('A', 51); // 51 characters

        // Act
        var result = _messageTemplatesValidationService.IsValidSubject(longSubject);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValidSubject_SubjectContainsSpecialCharacters_ReturnsFalse()
    {
        // Arrange
        const string subjectWithSpecialChars = "Invalid & Subject";

        // Act
        var result = _messageTemplatesValidationService.IsValidSubject(subjectWithSpecialChars);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValidLength_ValidBodyLength_ReturnsTrue()
    {
        // Arrange
        const string validBody = "Valid body";

        // Act
        var result = _messageTemplatesValidationService.IsValidLength(validBody);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValidLength_BodyTooShort_ReturnsFalse()
    {
        // Arrange
        const string shortBody = "abcd"; // 4 characters

        // Act
        var result = _messageTemplatesValidationService.IsValidLength(shortBody);

        // Assert
        Assert.That(result, Is.False);
    }
}