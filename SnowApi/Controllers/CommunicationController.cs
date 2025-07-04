using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnowApi.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SnowApi.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CommunicationController : ControllerBase
{
    private readonly ICommunicationService _communicationService;

    public CommunicationController(
        ICommunicationService communicationService)
    {
        _communicationService = communicationService;
    }

    /// <summary>
    /// Send template message to customer
    /// </summary>
    /// <param name="customerUniqueId">The unique identifier of the customer to whom the message will be sent.</param>
    /// <param name="templateId">The identifier of the message template to be used for sending the message.</param>
    /// <returns>Ok response</returns>
    /// <remarks>
    /// This function attempts to send a predefined message template to a specified customer.
    /// Ensure that the customerUniqueId corresponds to a valid customer and the templateId corresponds to an existing template.
    /// </remarks>
    /// <response code="200">If request was successful</response>
    /// <response code="400">If invalid request was made</response>
    /// <response code="500">If exception was raised</response>
    [SwaggerOperation("Send template message to customer")]
    [SwaggerResponse(200, "Dataset returned")]
    [HttpPost]
    [Route("communication/send_template_message")]
    public IActionResult SendTemplateMessageToCustomer(string customerUniqueId, int templateId)
    {
        try
        {
            Console.WriteLine("Sending template message to customer");

            var result = _communicationService.SendTemplateMessageToCustomer(customerUniqueId, templateId);

            if (result == "Succeeded")
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Failed to send template message to customer: " + result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception within SendTemplateMessage: " + e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failure");
        }
    }
}