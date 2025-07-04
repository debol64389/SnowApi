using Moq;
using NUnit.Framework;
using SnowApi.Core.Entities;
using SnowApi.Infrastructure.Interfaces;
using SnowApi.Services;
using SnowApi.Services.Interfaces;

namespace SnowApi.UnitTests.Services;

[TestFixture]
public class CustomersServiceTests
{
    private Mock<IRepositorySql> _mockRepositorySql;
    private Mock<ICustomerValidationService> _mockCustomerValidationService;
    private Mock<ICustomerUniqueIdFactory> _mockCustomerUniqueIdFactory;
    private CustomersService _customersService;

    [SetUp]
    public void SetUp()
    {
        _mockRepositorySql = new Mock<IRepositorySql>();
        _mockCustomerValidationService = new Mock<ICustomerValidationService>();
        _mockCustomerUniqueIdFactory = new Mock<ICustomerUniqueIdFactory>();
        _customersService = new CustomersService(
            _mockRepositorySql.Object,
            _mockCustomerValidationService.Object,
            _mockCustomerUniqueIdFactory.Object);
    }

    [Test]
    public void GetCustomerDetails_CustomerExists_ReturnsCustomerDetails()
    {
        // Arrange
        const string uniqueId = "123";
        var customer = new Customer { UniqueId = uniqueId, Name = "John Doe", EmailAddress = "john.doe@example.com" };
        _mockRepositorySql.Setup(r => r.PullCustomerDetails(uniqueId)).Returns(customer);

        // Act
        var result = _customersService.GetCustomerDetails(uniqueId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UniqueId, Is.EqualTo(customer.UniqueId));
        Assert.That(result.Name, Is.EqualTo(customer.Name));
        Assert.That(result.EmailAddress, Is.EqualTo(customer.EmailAddress));
    }

    [Test]
    public void GetCustomerDetails_CustomerDoesNotExist_ReturnsNull()
    {
        // Arrange
        const string uniqueId = "123";
        _mockRepositorySql.Setup(r => r.PullCustomerDetails(uniqueId)).Returns((Customer)null!);

        // Act
        var result = _customersService.GetCustomerDetails(uniqueId);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void AddNewCustomer_InvalidEmail_ReturnsInvalidEmailMessage()
    {
        // Arrange
        const string name = "Jane Doe";
        const string emailAddress = "invalid-email";
        _mockCustomerValidationService.Setup(v => v.IsValidEmail(emailAddress)).Returns(false);

        // Act
        var result = _customersService.AddNewCustomer(name, emailAddress);

        // Assert
        Assert.That(result, Is.EqualTo("Invalid email address provided."));
    }

    [Test]
    public void AddNewCustomer_InvalidName_ReturnsInvalidNameMessage()
    {
        // Arrange
        const string name = "";
        const string emailAddress = "jane.doe@example.com";
        _mockCustomerValidationService.Setup(v => v.IsValidEmail(emailAddress)).Returns(true);
        _mockCustomerValidationService.Setup(v => v.IsValidCustomerName(name)).Returns(false);

        // Act
        var result = _customersService.AddNewCustomer(name, emailAddress);

        // Assert
        Assert.That(result, Is.EqualTo("Invalid customer name provided."));
    }

    [Test]
    public void AddNewCustomer_ValidData_AddsCustomerSuccessfully()
    {
        // Arrange
        const string name = "Jane Doe";
        const string emailAddress = "jane.doe@example.com";
        const string uniqueId = "ABC123";
        _mockCustomerValidationService.Setup(v => v.IsValidEmail(emailAddress)).Returns(true);
        _mockCustomerValidationService.Setup(v => v.IsValidCustomerName(name)).Returns(true);
        _mockCustomerUniqueIdFactory.Setup(f => f.GenerateUniqueId()).Returns(uniqueId);

        // Act
        var result = _customersService.AddNewCustomer(name, emailAddress);

        // Assert
        Assert.That(result, Is.EqualTo("Succeeded"));
    }

    [Test]
    public void DeleteCustomer_CustomerDoesNotExist_ReturnsCustomerDoesNotExistMessage()
    {
        // Arrange
        const string uniqueId = "123";
        _mockRepositorySql.Setup(r => r.PullCustomerDetails(uniqueId)).Returns((Customer)null!);

        // Act
        var result = _customersService.DeleteCustomer(uniqueId);

        // Assert
        Assert.That(result, Is.EqualTo("Customer doesn't exist!"));
    }

    [Test]
    public void DeleteCustomer_CustomerExists_DeletesCustomerSuccessfully()
    {
        // Arrange
        const string uniqueId = "123";
        var customer = new Customer { Id = 1, UniqueId = uniqueId, Name = "John Smith", EmailAddress = "j.smith@msm.com"};
        _mockRepositorySql.Setup(r => r.PullCustomerDetails(uniqueId)).Returns(customer);

        // Act
        var result = _customersService.DeleteCustomer(uniqueId);

        // Assert
        Assert.That(result, Is.EqualTo("Succeeded"));
    }

    [Test]
    public void UpdateCustomerEmail_CustomerDoesNotExist_ReturnsCustomerDoesNotExistMessage()
    {
        // Arrange
        const string uniqueId = "123";
        const string newEmailAddress = "new.email@example.com";
        _mockRepositorySql.Setup(r => r.PullCustomerDetails(uniqueId)).Returns((Customer)null!);

        // Act
        var result = _customersService.UpdateCustomerEmail(uniqueId, newEmailAddress);

        // Assert
        Assert.That(result, Is.EqualTo("Customer doesn't exist!"));
    }

    [Test]
    public void UpdateCustomerEmail_InvalidEmail_ReturnsInvalidEmailMessage()
    {
        // Arrange
        const string uniqueId = "123";
        const string newEmailAddress = "invalid-email";
        var customer = new Customer { Id = 1, UniqueId = uniqueId, Name = "John Smith", EmailAddress = "j.smith@msm.com" };
        _mockRepositorySql.Setup(r => r.PullCustomerDetails(uniqueId)).Returns(customer);
        _mockCustomerValidationService.Setup(v => v.IsValidEmail(newEmailAddress)).Returns(false);

        // Act
        var result = _customersService.UpdateCustomerEmail(uniqueId, newEmailAddress);

        // Assert
        Assert.That(result, Is.EqualTo("Invalid email address provided."));
    }

    [Test]
    public void UpdateCustomerEmail_ValidData_UpdatesEmailSuccessfully()
    {
        // Arrange
        const string uniqueId = "123";
        const string newEmailAddress = "new.email@example.com";
        var customer = new Customer { Id = 1, UniqueId = uniqueId, Name = "John Smith", EmailAddress = "j.smith@msm.com" };
        _mockRepositorySql.Setup(r => r.PullCustomerDetails(uniqueId)).Returns(customer);
        _mockCustomerValidationService.Setup(v => v.IsValidEmail(newEmailAddress)).Returns(true);

        // Act
        var result = _customersService.UpdateCustomerEmail(uniqueId, newEmailAddress);

        // Assert
        Assert.That(result, Is.EqualTo("Succeeded"));
    }
}