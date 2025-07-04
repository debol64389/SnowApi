using SnowApi.Core.DTOs;
using SnowApi.Core.Entities;
using SnowApi.Infrastructure.Interfaces;
using SnowApi.Services.Interfaces;

namespace SnowApi.Services;

public class CustomersService : ICustomersService
{
    private readonly IRepositorySql _repositorySql;
    private readonly ICustomerValidationService _customerValidationService;
    private readonly ICustomerUniqueIdFactory _customerUniqueIdFactory;

    public CustomersService(
        IRepositorySql repositorySql,
        ICustomerValidationService customerValidationService,
        ICustomerUniqueIdFactory customerUniqueIdFactory)
    {
        _repositorySql = repositorySql;
        _customerValidationService = customerValidationService;
        _customerUniqueIdFactory = customerUniqueIdFactory;
    }

    /// <summary>
    /// Pulls customer details from the database based on the unique ID
    /// </summary>
    /// <param name="uniqueId"></param>
    /// <returns>Details of customer</returns>
    public CustomerDetailsDto? GetCustomerDetails(string uniqueId)
    {
        var customer = _repositorySql.PullCustomerDetails(uniqueId);
        if (customer is null)
        {
            return null;
        }

        var customerDetails = new CustomerDetailsDto
        {
            UniqueId = customer.UniqueId,
            Name = customer.Name,
            EmailAddress = customer.EmailAddress
        };

        return customerDetails;
    }

    /// <summary>
    /// Adds a new customer to the database
    /// Generates a unique ID for the customer and validates the provided name and email address
    /// </summary>
    /// <param name="name"></param>
    /// <param name="emailAddress"></param>
    /// <returns>Message if succeeded or failed</returns>
    public string AddNewCustomer(string name, string emailAddress)
    {
        var isValidEmail = _customerValidationService.IsValidEmail(emailAddress);
        if (!isValidEmail)
        {
            Console.WriteLine("Invalid email address provided.");
            return "Invalid email address provided.";
        }

        var isValidCustomerName = _customerValidationService.IsValidCustomerName(name);
        if (!isValidCustomerName)
        {
            Console.WriteLine("Invalid customer name provided.");
            return "Invalid customer name provided.";
        }

        var uniqueId = _customerUniqueIdFactory.GenerateUniqueId();

        var customer = new Customer
        {
            UniqueId = uniqueId,
            Name = name,
            EmailAddress = emailAddress
        };

        _repositorySql.AddCustomer(customer);
        return "Succeeded";
    }

    /// <summary>
    /// Sets customer as deleted in the database
    /// </summary>
    /// <param name="uniqueId"></param>
    /// <returns>Message if succeeded or failed</returns>
    public string DeleteCustomer(string uniqueId)
    {
        var customer = _repositorySql.PullCustomerDetails(uniqueId);
        if (customer is null)
        {
            Console.WriteLine("Customer doesn't exist!");
            return "Customer doesn't exist!";
        }

        _repositorySql.DeleteCustomer(uniqueId);
        return "Succeeded";
    }

    /// <summary>
    /// Updates customer email address in the database
    /// </summary>
    /// <param name="uniqueId"></param>
    /// <param name="newEmailAddress"></param>
    /// <returns>Message if succeeded or failed</returns>
    public string UpdateCustomerEmail(string uniqueId, string newEmailAddress)
    {
        var customer = _repositorySql.PullCustomerDetails(uniqueId);
        if (customer is null)
        {
            Console.WriteLine("Customer doesn't exist!");
            return "Customer doesn't exist!";
        }

        var isValidEmail = _customerValidationService.IsValidEmail(newEmailAddress);
        if (!isValidEmail)
        {
            Console.WriteLine("Invalid email address provided.");
            return "Invalid email address provided.";
        }

        _repositorySql.UpdateCustomerEmail(uniqueId, newEmailAddress);
        return "Succeeded";
    }
}