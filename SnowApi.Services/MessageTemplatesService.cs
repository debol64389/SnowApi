using SnowApi.Core.DTOs;
using SnowApi.Core.Entities;
using SnowApi.Infrastructure.Interfaces;
using SnowApi.Services.Interfaces;

namespace SnowApi.Services;

public class MessageTemplatesService : IMessageTemplatesService
{
    private readonly IRepositorySql _repositorySql;
    private readonly IMessageTemplatesValidationService _messageTemplatesValidationService;

    public MessageTemplatesService(
        IRepositorySql repositorySql,
        IMessageTemplatesValidationService messageTemplatesValidationService)
    {
        _repositorySql = repositorySql;
        _messageTemplatesValidationService = messageTemplatesValidationService;
    }

    /// <summary>
    /// Pull the message template details by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Details of template</returns>
    public MessageTemplateDetailsDto? GetMessageTemplateDetails(int id)
    {
        var messageTemplate = _repositorySql.PullMessageTemplateDetails(id);
        if (messageTemplate is null)
        {
            return null;
        }

        var messageTemplateDetails = new MessageTemplateDetailsDto
        {
            Id = messageTemplate.Id,
            Name = messageTemplate.Name,
            Subject = messageTemplate.Subject,
            Body = messageTemplate.Body
        };

        return messageTemplateDetails;
    }

    /// <summary>
    /// Adds a new message template to the database
    /// </summary>
    /// <param name="name"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns>Message if succeeded or failed</returns>
    public string AddNewMessageTemplate(string name, string subject, string body)
    {
        var isValidName = _messageTemplatesValidationService.IsValidLength(subject);
        if (!isValidName)
        {
            Console.WriteLine("Invalid name provided.");
            return "Invalid name provided.";
        }

        var isValidSubject = _messageTemplatesValidationService.IsValidSubject(subject);
        if (!isValidSubject)
        {
            Console.WriteLine("Invalid subject provided.");
            return "Invalid subject provided.";
        }

        var isValidBody = _messageTemplatesValidationService.IsValidLength(body);
        if (!isValidBody)
        {
            Console.WriteLine("Invalid body provided.");
            return "Invalid body provided.";
        }

        var messageTemplate = new MessageTemplate
        {
            Name = name,
            Subject = subject,
            Body = body
        };

        _repositorySql.AddMessageTemplate(messageTemplate);
        return "Succeeded";
    }

    /// <summary>
    /// Updates the subject of an existing message template
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newSubject"></param>
    /// <returns>Message if succeeded or failed</returns>
    public string UpdateMessageTemplateSubject(int id, string newSubject)
    {
        var messageTemplate = _repositorySql.PullMessageTemplateDetails(id);
        if (messageTemplate is null)
        {
            Console.WriteLine("Message template doesn't exist!");
            return "Message template doesn't exist!";
        }

        var isValidSubject = _messageTemplatesValidationService.IsValidSubject(newSubject);
        if (!isValidSubject)
        {
            Console.WriteLine("Invalid subject provided.");
            return "Invalid subject provided.";
        }

        _repositorySql.UpdateMessageTemplateSubject(id, newSubject);
        return "Succeeded";
    }

    /// <summary>
    /// Updates the body of an existing message template
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newBody"></param>
    /// <returns>Message if succeeded or failed</returns>
    public string UpdateMessageTemplateBody(int id, string newBody)
    {
        var messageTemplate = _repositorySql.PullMessageTemplateDetails(id);
        if (messageTemplate is null)
        {
            Console.WriteLine("Message template doesn't exist!");
            return "Message template doesn't exist!";
        }

        var isValidBody = _messageTemplatesValidationService.IsValidLength(newBody);
        if (!isValidBody)
        {
            Console.WriteLine("Invalid body provided.");
            return "Invalid body provided.";
        }

        _repositorySql.UpdateMessageTemplateBody(id, newBody);
        return "Succeeded";
    }

    /// <summary>
    /// Sets message template as deleted in the database
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Message if succeeded or failed</returns>
    public string DeleteMessageTemplate(int id)
    {
        var messageTemplate = _repositorySql.PullMessageTemplateDetails(id);
        if (messageTemplate is null)
        {
            Console.WriteLine("Message template doesn't exist!");
            return "Message template doesn't exist!";
        }

        _repositorySql.DeleteMessageTemplate(id);
        return "Succeeded";
    }
}