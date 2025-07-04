namespace SnowApi.Services.Interfaces;

public interface IMessageTemplatesValidationService
{
    bool IsValidSubject(string subject);
    bool IsValidLength(string body);
}