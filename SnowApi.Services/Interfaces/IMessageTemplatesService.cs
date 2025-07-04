using SnowApi.Core.DTOs;

namespace SnowApi.Services.Interfaces;

public interface IMessageTemplatesService
{
    MessageTemplateDetailsDto? GetMessageTemplateDetails(int id);
    string AddNewMessageTemplate(string name, string subject, string body);
    string UpdateMessageTemplateSubject(int id, string newSubject);
    string UpdateMessageTemplateBody(int id, string newBody);
    string DeleteMessageTemplate(int id);
}