using SnowApi.Core.DTOs;

namespace SnowApi.Services.Interfaces;

public interface ICustomersService
{
   CustomerDetailsDto? GetCustomerDetails(string uniqueId);
   string AddNewCustomer(string name, string emailAddress);
   string DeleteCustomer(string uniqueId);
   string UpdateCustomerEmail(string uniqueId, string newEmailAddress);
}