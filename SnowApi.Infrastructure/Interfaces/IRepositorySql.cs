using SnowApi.Core.Entities;

namespace SnowApi.Infrastructure.Interfaces;

public interface IRepositorySql
{
    Customer? PullCustomerDetails(string uniqueId);
    void AddCustomer(Customer customer);
    void DeleteCustomer(string uniqueId);
    void UpdateCustomerEmail(string uniqueId, string newEmailAddress);
    MessageTemplate? PullMessageTemplateDetails(int id);
    void AddMessageTemplate(MessageTemplate messageTemplate);
    void UpdateMessageTemplateSubject(int id, string newSubject);
    void UpdateMessageTemplateBody(int id, string newBody);
    void DeleteMessageTemplate(int id);
    void SaveMessageThatWasSent(int messageTemplateId, string customerUniqueId, string message);
}