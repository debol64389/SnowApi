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
public class MessageTemplatesController : ControllerBase
{
    private readonly IMessageTemplatesService _messageTemplatesService;

    public MessageTemplatesController(
        IMessageTemplatesService messageTemplatesService)
    {
        _messageTemplatesService = messageTemplatesService;
    }

    /// <summary>
    /// Add new message template
    /// </summary>
    /// <param name="name">Name of the template to be saved.</param>
    /// <param name="subject">Subject of the template to be saved.</param>
    /// <param name="body">Body of the template to be saved.</param>
    /// <returns>Ok response</returns>
    /// <remarks>
    /// This function attempts to save a new message template with the provided name, subject and body.
    /// Ensure that parameters provided are valid.
    /// Body can contain placeholders such {CustomerName}, {UniqueId} and {EmailAddress} that are getting replaced by customer info.
    /// </remarks>
    /// <response code="200">If request was successful</response>
    /// <response code="400">If invalid request was made</response>
    /// <response code="500">If exception was raised</response>
    [SwaggerOperation("Add new message template")]
    [SwaggerResponse(200, "Dataset returned")]
    [HttpPost]
    [Route("message_templates/add_new")]
    public IActionResult AddNewMessageTemplate(string name, string subject, string body)
    {
        try
        {
            Console.WriteLine("Adding new message template");

            var result = _messageTemplatesService.AddNewMessageTemplate(name, subject, body);

            if (result == "Succeeded")
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Failed to add new message template: " + result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception within AddNewMessageTemplate: " + e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failure");
        }
    }

    /// <summary>
    /// Returns details of a message template by ID
    /// </summary>
    /// <param name="id">The unique identifier of the message template.</param>
    /// <returns>Details of a message template</returns>
    /// <remarks>
    /// This function attempts to return details of a message template by its ID.
    /// Ensure that the id corresponds to a valid template.
    /// </remarks>
    /// <response code="200">If request was successful</response>
    /// <response code="400">If invalid request was made</response>
    /// <response code="500">If exception was raised</response>
    [SwaggerOperation("Fetch message template Id")]
    [SwaggerResponse(200, "Dataset returned")]
    [HttpGet]
    [Route("message_template/get_details")]
    public IActionResult GetMessageTemplateDetails(int id)
    {
        try
        {
            Console.WriteLine("Retrieving message template details for Id: " + id);

            var result = _messageTemplatesService.GetMessageTemplateDetails(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(new OkResponse<MessageTemplateDetailsDto>(result));
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception within GetMessageTemplateDetails: " + e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failure");
        }
    }

    /// <summary>
    /// Update message template subject
    /// </summary>
    /// <param name="id">The unique identifier of the message template.</param>
    /// <param name="newSubject">New subject for the message template.</param>
    /// <returns>Ok response</returns>
    /// <remarks>
    /// This function attempts to update the subject of a message template by its ID.
    /// Ensure that the id corresponds to a valid template.
    /// </remarks>
    /// <response code="200">If request was successful</response>
    /// <response code="400">If invalid request was made</response>
    /// <response code="500">If exception was raised</response>
    [SwaggerOperation("Update message template subject")]
    [SwaggerResponse(200, "Dataset returned")]
    [HttpPatch]
    [Route("message_template/update_subject")]
    public IActionResult UpdateMessageTemplateSubject(int id, string newSubject)
    {
        try
        {
            Console.WriteLine("Updating message template subject");

            var result = _messageTemplatesService.UpdateMessageTemplateSubject(id, newSubject);

            if (result == "Succeeded")
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Failed to update message template subject: " + result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception within UpdateMessageTemplateSubject: " + e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failure");
        }
    }

    /// <summary>
    /// Update message template body
    /// </summary>
    /// <param name="id">The unique identifier of the message template.</param>
    /// <param name="newBody">New body for the message template.</param>
    /// <returns>Ok response</returns>
    /// <remarks>
    /// This function attempts to update the body of a message template by its ID.
    /// Ensure that the id corresponds to a valid template.
    /// </remarks>
    /// <response code="200">If request was successful</response>
    /// <response code="400">If invalid request was made</response>
    /// <response code="500">If exception was raised</response>
    [SwaggerOperation("Update message template body")]
    [SwaggerResponse(200, "Dataset returned")]
    [HttpPatch]
    [Route("message_template/update_body")]
    public IActionResult UpdateMessageTemplateBody(int id, string newBody)
    {
        try
        {
            Console.WriteLine("Updating message template body");

            var result = _messageTemplatesService.UpdateMessageTemplateBody(id, newBody);

            if (result == "Succeeded")
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Failed to update message template body: " + result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception within UpdateMessageTemplateBody: " + e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failure");
        }
    }

    /// <summary>
    /// Delete message template
    /// </summary>
    /// <param name="id">The unique identifier of the message template.</param>
    /// <returns>Ok response</returns>
    /// <remarks>
    /// This function attempts to delete a message template by its ID.
    /// Ensure that the id corresponds to a valid template.
    /// </remarks>
    /// <response code="200">If request was successful</response>
    /// <response code="404">If invalid request was made</response>
    /// <response code="500">If exception was raised</response>
    [SwaggerOperation("Delete message template")]
    [SwaggerResponse(200, "Dataset returned")]
    [HttpPatch]
    [Route("message_template/delete")]
    public IActionResult DeleteMessageTemplate(int id)
    {
        try
        {
            Console.WriteLine("Deleting message template");

            var result = _messageTemplatesService.DeleteMessageTemplate(id);

            if (result == "Succeeded")
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status404NotFound, "Failed to delete message template: " + result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception within DeleteMessageTemplate: " + e);
            return StatusCode(StatusCodes.Status500InternalServerError, "Failure");
        }
    }
}