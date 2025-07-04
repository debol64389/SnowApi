using SnowApi.Services.Interfaces;

namespace SnowApi.Services;

public class CustomerValidationService : ICustomerValidationService
{
    /// <summary>
    /// Validates the provided email address
    /// </summary>
    /// <param name="email"></param>
    /// <returns>Validation status</returns>
    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }
        try
        {
            var address = new System.Net.Mail.MailAddress(email);
            return address.Address == email;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validates the provided customer name
    /// Check length requirements - minimum 2 characters, maximum 50 characters
    /// Check for valid characters - only letters and spaces
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Validation status</returns>
    public bool IsValidCustomerName(string name)
    {
        if (name.Length is < 2 or > 50)
        {
            return false;
        }

        foreach (var c in name)
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            {
                return false;
            }
        }

        return true;
    }
}