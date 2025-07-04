namespace SnowApi.Services.Interfaces;

public interface ICommunicationService
{
    string SendTemplateMessageToCustomer(string customerUniqueId, int templateId);
}