using NUnit.Framework;
using SnowApi.Services;

namespace SnowApi.UnitTests.Services;

[TestFixture]
public class CustomerValidationServiceTests
{
    private CustomerValidationService _customerValidationService;

    [SetUp]
    public void SetUp()
    {
        _customerValidationService = new CustomerValidationService();
    }

    [Test]
    public void IsValidEmail_ValidEmail_ReturnsTrue()
    {
        // Arrange
        const string validEmail = "test@example.com";

        // Act
        var result = _customerValidationService.IsValidEmail(validEmail);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValidEmail_EmptyEmail_ReturnsFalse()
    {
        // Arrange
        const string emptyEmail = "";

        // Act
        var result = _customerValidationService.IsValidEmail(emptyEmail);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValidEmail_NullEmail_ReturnsFalse()
    {
        // Arrange
        string nullEmail = null!;

        // Act
        var result = _customerValidationService.IsValidEmail(nullEmail);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValidEmail_InvalidEmailFormat_ReturnsFalse()
    {
        // Arrange
        const string invalidEmail = "invalid-email";

        // Act
        var result = _customerValidationService.IsValidEmail(invalidEmail);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValidCustomerName_ValidName_ReturnsTrue()
    {
        // Arrange
        const string validName = "John Doe";

        // Act
        var result = _customerValidationService.IsValidCustomerName(validName);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValidCustomerName_NameTooShort_ReturnsFalse()
    {
        // Arrange
        const string shortName = "J";

        // Act
        var result = _customerValidationService.IsValidCustomerName(shortName);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValidCustomerName_NameTooLong_ReturnsFalse()
    {
        // Arrange
        var longName = new string('A', 51); // 51 characters

        // Act
        var result = _customerValidationService.IsValidCustomerName(longName);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValidCustomerName_NameWithInvalidCharacters_ReturnsFalse()
    {
        // Arrange
        const string nameWithInvalidChars = "John Doe!";

        // Act
        var result = _customerValidationService.IsValidCustomerName(nameWithInvalidChars);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValidCustomerName_NameWithSpaces_ReturnsTrue()
    {
        // Arrange
        const string nameWithSpaces = "John Doe";

        // Act
        var result = _customerValidationService.IsValidCustomerName(nameWithSpaces);

        // Assert
        Assert.That(result, Is.True);
    }
}