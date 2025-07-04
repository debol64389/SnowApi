using SnowApi.Services.Interfaces;

namespace SnowApi.Services;

public class MessageTemplatesValidationService : IMessageTemplatesValidationService
{
    /// <summary>
    /// Check length requirements - minimum 2 characters, maximum 50 characters
    /// Check if the subject contains any of the special characters
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    public bool IsValidSubject(string subject)
    {
        if (subject.Length is < 2 or > 50)
        {
            return false;
        }

        // Define the special characters to check for
        char[] specialCharacters = ['*', '%', '&', '#', '^'];

        foreach (var c in specialCharacters)
        {
            if (subject.Contains(c))
            {
                return false; 
            }
        }

        return true;
    }

    /// <summary>
    /// Check length requirements of email's body - minimum 5 characters
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    public bool IsValidLength(string body)
    {
        return body.Length >= 5;
    }
}