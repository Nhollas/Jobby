using Jobby.Application.Features.ContactFeatures.Commands.Create;
using Jobby.Application.Features.ContactFeatures.Commands.Delete;
using Jobby.Application.Features.ContactFeatures.Commands.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContactController : ApiController
{
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Create", Name = "CreateContact")]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand command)
    {
        var contactId = await Sender.Send(command);
        return CreatedAtAction(nameof(CreateContact), contactId);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpDelete("Delete/{contactId:guid}", Name = "DeleteContact")]
    public async Task<IActionResult> DeleteContact(Guid contactId)
    {
        await Sender.Send(new DeleteContactCommand(contactId));
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpPut("Update", Name = "UpdateContact")]
    public async Task<IActionResult> UpdateContact([FromBody] UpdateContactCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }
}
