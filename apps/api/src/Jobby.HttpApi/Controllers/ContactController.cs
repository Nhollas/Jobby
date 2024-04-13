using Jobby.Application.Dtos;
using Jobby.Application.Features.ContactFeatures.Commands.Create;
using Jobby.Application.Features.ContactFeatures.Commands.Delete;
using Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.ContactFeatures.Queries.GetById;
using Jobby.Application.Features.ContactFeatures.Queries.GetList;
using Jobby.Application.Responses.Common;
using Jobby.HttpApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ContactController : ApiController
{
    [HttpGet("{contactReference}", Name = "GetContact")]
    public async Task<ActionResult<ContactDto>> GetContact(string contactReference)
    {
        GetContactDetailQuery contactQuery = new GetContactDetailQuery(contactReference);
        return Ok(await Sender.Send(contactQuery));
    }
    
    [HttpPost]
    public async Task<ActionResult<ContactDto>> CreateContact([FromBody] CreateContactCommand command)
    {
        BaseResult<ContactDto, CreateContactOutcomes>? createdContact = await Sender.Send(command);
        return CreatedAtAction(nameof(CreateContact), createdContact);
    }

    [HttpDelete("{contactReference}")]
    public async Task<ActionResult<DeleteContactResponse>> DeleteContact(string contactReference)
    {
        await Sender.Send(new DeleteContactCommand(contactReference));
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<ContactDto>> UpdateContact([FromBody] UpdateContactCommand command)
    {
        BaseResult<ContactDto, UpdateContactOutcomes>? updatedContact =  await Sender.Send(command);
        return Ok(updatedContact);
    }
    
    [HttpGet("/contacts", Name = "ListContacts")]
    public async Task<ActionResult<List<ContactDto>>> GetAllContacts()
    {
        GetContactListQuery contactQuery = new GetContactListQuery();
        return Ok(await Sender.Send(contactQuery));
    }
}
