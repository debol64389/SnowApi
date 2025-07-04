using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnowApi.Core.DTOs;
using SnowApi.Core.Responces;
using SnowApi.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SnowApi.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly ICustomersService _customersService;

    public CustomersController(
        ICustomersService customersService)
    {
        _customersService = customersService;
    }

    /// <summary>
    /// Add new customer
    /// </summary>
    /// <param name="name">Name of the customer to be saved.</param>
    /// <param name="emailAddress">Email address of the customer to be saved.</param>
    /// <returns>Ok response</returns>
    /// <remarks>
    /// This function attempts to save a new customer with the provided name and email address.
    /// Ensure that the name and email address are valid.
    /// </remarks>
    /// <response code="200">If request was successful</response>
    /// <response code="400">If invalid request was made</response>
    /// <response code="500">If exception was raised</response>
    [SwaggerOperation("Add new customer")]
    [SwaggerResponse(200, "Dataset returned")]
    [HttpPost]
    [Route("customers/add_new")]
    public IActionResult AddNewCustomer(string name, string emailAddress)
    {
        try
        {
            Console.WriteLine("Adding new customer");

            var result = _customersService.AddNewCustomer(name, emailAddress);

            if (result == "Succeeded")
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Failed to add new customer: " + result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception within GetCustomerDetails: " + e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failure");
        }
    }

    /// <summary>
    /// Returns details of a customer by Unique ID
    /// </summary>
    /// <param name="uniqueId">The unique identifier of the customer.</param>
    /// <returns>Details of a customer</returns>
    /// <remarks>
    /// This function attempts to return details of a customer with the provided unique ID.
    /// Ensure that the customerUniqueId corresponds to a valid customer.
    /// </remarks>
    /// <response code="200">If request was successful</response>
    /// <response code="400">If invalid request was made</response>
    /// <response code="500">If exception was raised</response>
    [SwaggerOperation("Fetch customer unique Id")]
    [SwaggerResponse(200, "Dataset returned")]
    [HttpGet]
    [Route("customers/get_details")]
    public IActionResult GetCustomerDetails(string uniqueId) 
    {
        try
        {
            Console.WriteLine("Retrieving customer details for Unique Id: " + uniqueId);

            var result = _customersService.GetCustomerDetails(uniqueId);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(new OkResponse<CustomerDetailsDto>(result));
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception within GetCustomerDetails: " + e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failure");
        }
    }

    /// <summary>
    /// Update customer
    /// </summary>
    /// <param name="uniqueId">The unique identifier of the customer.</param>
    /// <param name="newEmailAddress">New email address of the customer to be saved.</param>
    /// <returns>Ok response</returns>
    /// <remarks>
    /// This function attempts to return details of a customer with the provided unique ID.
    /// Ensure that the customerUniqueId corresponds to a valid customer and the newEmailAddress corresponds to a valid email.
    /// </remarks>
    /// <response code="200">If request was successful</response>
    /// <response code="404">If invalid request was made</response>
    /// <response code="500">If exception was raised</response>
    [SwaggerOperation("Update customer email")]
    [SwaggerResponse(200, "Dataset returned")]
    [HttpPatch]
    [Route("customers/update_email")]
    public IActionResult UpdateCustomer(string uniqueId, string newEmailAddress)
    {
        try
        {
            Console.WriteLine("Updating customer email");

            var result = _customersService.UpdateCustomerEmail(uniqueId, newEmailAddress);

            if (result == "Succeeded")
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status404NotFound, "Failed to update customer email: " + result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception within UpdateCustomerEmail: " + e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failure");
        }
    }

    /// <summary>
    /// Delete customer
    /// </summary>
    /// <param name="uniqueId">The unique identifier of the customer.</param>
    /// <returns>Ok response</returns>
    /// <remarks>
    /// This function attempts to set customer's status to deleted with provided unique ID.
    /// Ensure that the customerUniqueId corresponds to a valid customer.
    /// </remarks>
    /// <response code="200">If request was successful</response>
    /// <response code="404">If invalid request was made</response>
    /// <response code="500">If exception was raised</response>
    [SwaggerOperation("Delete customer")]
    [SwaggerResponse(200, "Dataset returned")]
    [HttpPatch]
    [Route("customers/delete")]
    public IActionResult DeleteCustomer(string uniqueId)
    {
        try
        {
            Console.WriteLine("Deleting customer");

            var result = _customersService.DeleteCustomer(uniqueId);

            if (result == "Succeeded")
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status404NotFound, "Failed to delete customer: " + result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception within DeleteCustomer: " + e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failure");
        }
    }
}