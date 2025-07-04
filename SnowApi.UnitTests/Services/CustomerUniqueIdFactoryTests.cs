using NUnit.Framework;
using SnowApi.Services;

namespace SnowApi.UnitTests.Services;

[TestFixture]
public class CustomerUniqueIdFactoryTests
{
    private CustomerUniqueIdFactory _customerUniqueIdFactory;

    [SetUp]
    public void SetUp()
    {
        _customerUniqueIdFactory = new CustomerUniqueIdFactory();
    }

    [Test]
    public void GenerateUniqueId_ReturnsStringWithTwoLettersFollowedByFourDigits()
    {
        // Act
        var uniqueId = _customerUniqueIdFactory.GenerateUniqueId();

        // Assert
        Assert.That(uniqueId, Is.Not.Null);
        Assert.That(uniqueId.Length, Is.EqualTo(6));
        Assert.That(char.IsLetter(uniqueId[0]), "First character should be a letter.");
        Assert.That(char.IsLetter(uniqueId[1]), "Second character should be a letter.");
        Assert.That(char.IsDigit(uniqueId[2]), "Third character should be a digit.");
        Assert.That(char.IsDigit(uniqueId[3]), "Fourth character should be a digit.");
        Assert.That(char.IsDigit(uniqueId[4]), "Fifth character should be a digit.");
        Assert.That(char.IsDigit(uniqueId[5]), "Sixth character should be a digit.");
    }

    [Test]
    public void GenerateUniqueId_ReturnsDifferentValuesOnMultipleCalls()
    {
        // Act
        var uniqueId1 = _customerUniqueIdFactory.GenerateUniqueId();
        var uniqueId2 = _customerUniqueIdFactory.GenerateUniqueId();

        // Assert
        Assert.That(uniqueId1, Is.Not.EqualTo(uniqueId2), "Unique IDs on multiple calls should be different.");
    }
}