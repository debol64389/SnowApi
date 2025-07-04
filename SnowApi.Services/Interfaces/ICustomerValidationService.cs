namespace SnowApi.Services.Interfaces;

public interface ICustomerValidationService
{
    bool IsValidEmail(string email);
    bool IsValidCustomerName(string name);
}