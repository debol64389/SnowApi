using SnowApi.Infrastructure.Interfaces;
using SnowApi.Services.Interfaces;

namespace SnowApi.Services;

public class CommunicationService : ICommunicationService
{
    private readonly IRepositorySql _repositorySql;

    public CommunicationService(
        IRepositorySql repositorySql)
    {
        _repositorySql = repositorySql;
    }

    /// <summary>
    /// Pulls customer details and message template from the database
    /// Then replaces placeholders in the message template with actual customer details
    /// Logs the message to the console and saves it to the database
    /// </summary>
    /// <param name="customerUniqueId"></param>
    /// <param name="templateId"></param>
    /// <returns>Message if succeeded or failed</returns>
    public string SendTemplateMessageToCustomer(string customerUniqueId, int templateId)
    {
        var customer = _repositorySql.PullCustomerDetails(customerUniqueId);
        if (customer is null)
        {
            Console.WriteLine("Customer doesn't exist!");
            return "Customer doesn't exist!";
        }

        var messageTemplate = _repositorySql.PullMessageTemplateDetails(templateId);
        if (messageTemplate is null)
        {
            Console.WriteLine("Message template doesn't exist!");
            return "Message template doesn't exist!";
        }

        messageTemplate.Body = messageTemplate.Body.Replace("{CustomerName}", customer.Name);
        messageTemplate.Body = messageTemplate.Body.Replace("{EmailAddress}", customer.EmailAddress);
        messageTemplate.Body = messageTemplate.Body.Replace("{UniqueId}", customer.UniqueId);

        // Simulate sending the message
        Console.WriteLine($"Sending message to {customer.EmailAddress}\n" +
                          $"Subject: '{messageTemplate.Subject}'\n" +
                          $"Body: '{messageTemplate.Body}'");

        _repositorySql.SaveMessageThatWasSent(messageTemplate.Id, customer.UniqueId, messageTemplate.Body);

        return "Succeeded";
    }
}