using Moq;
using NUnit.Framework;
using SnowApi.Core.Entities;
using SnowApi.Infrastructure.Interfaces;
using SnowApi.Services;

namespace SnowApi.UnitTests.Services;

[TestFixture]
public class CommunicationServiceTests
{
    private Mock<IRepositorySql> _mockRepositorySql;
    private CommunicationService _communicationService;

    [SetUp]
    public void SetUp()
    {
        _mockRepositorySql = new Mock<IRepositorySql>();
        _communicationService = new CommunicationService(_mockRepositorySql.Object);
    }

    [Test]
    public void SendTemplateMessageToCustomer_CustomerDoesNotExist_ReturnsCustomerDoesNotExistMessage()
    {
        // Arrange
        const string customerUniqueId = "123";
        const int templateId = 1;
        _ = _mockRepositorySql.Setup(r => r.PullCustomerDetails(customerUniqueId)).Returns((Customer)null!);

        // Act
        var result = _communicationService.SendTemplateMessageToCustomer(customerUniqueId, templateId);

        // Assert
        Assert.That(result, Is.EqualTo("Customer doesn't exist!"));
    }

    [Test]
    public void SendTemplateMessageToCustomer_TemplateDoesNotExist_ReturnsTemplateDoesNotExistMessage()
    {
        // Arrange
        const string customerUniqueId = "123";
        const int templateId = 1;
        var customer = new Customer { UniqueId = customerUniqueId, Name = "John Doe", EmailAddress = "john.doe@example.com" };
        _mockRepositorySql.Setup(r => r.PullCustomerDetails(customerUniqueId)).Returns(customer);
        _mockRepositorySql.Setup(r => r.PullMessageTemplateDetails(templateId)).Returns((MessageTemplate)null!);

        // Act
        var result = _communicationService.SendTemplateMessageToCustomer(customerUniqueId, templateId);

        // Assert
        Assert.That(result, Is.EqualTo("Message template doesn't exist!"));
    }

    [Test]
    public void SendTemplateMessageToCustomer_ValidCustomerAndTemplate_SendsMessageSuccessfully()
    {
        // Arrange
        const string customerUniqueId = "123";
        const int templateId = 1;
        var customer = new Customer { UniqueId = customerUniqueId, Name = "John Doe", EmailAddress = "john.doe@example.com" };
        var messageTemplate = new MessageTemplate { Id = templateId, Name = "Test", Subject = "Hello {CustomerName}", Body = "Your email is {EmailAddress}" };

        _mockRepositorySql.Setup(r => r.PullCustomerDetails(customerUniqueId)).Returns(customer);
        _mockRepositorySql.Setup(r => r.PullMessageTemplateDetails(templateId)).Returns(messageTemplate);
        _mockRepositorySql.Setup(r => r.SaveMessageThatWasSent(templateId, customerUniqueId, It.IsAny<string>())).Verifiable();

        // Act
        var result = _communicationService.SendTemplateMessageToCustomer(customerUniqueId, templateId);

        // Assert
        Assert.That(result, Is.EqualTo("Succeeded"));
    }
}