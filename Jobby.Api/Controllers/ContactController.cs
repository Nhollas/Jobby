using Jobby.Api.Controllers.Base;
using Jobby.Application.Features.ContactFeatures.Commands.Create;
using Jobby.Application.Features.ContactFeatures.Commands.Delete;
using Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContactController : ApiController
{
    [HttpPost("Create", Name = "CreateContact")]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand command)
    {
        var createdContact = await Sender.Send(command);
        return CreatedAtAction(nameof(CreateContact), createdContact);
    }

    [HttpDelete("Delete/{contactId:guid}", Name = "DeleteContact")]
    public async Task<IActionResult> DeleteContact(Guid contactId)
    {
        await Sender.Send(new DeleteContactCommand(contactId));
        return NoContent();
    }

    [HttpPut("Update", Name = "UpdateContact")]
    public async Task<IActionResult> UpdateContact([FromBody] UpdateContactCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }
}
