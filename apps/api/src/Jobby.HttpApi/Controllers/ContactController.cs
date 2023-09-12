﻿using Jobby.Application.Contracts.Contact;
using Jobby.Application.Features.ContactFeatures.Commands.Create;
using Jobby.Application.Features.ContactFeatures.Commands.Delete;
using Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.ContactFeatures.Queries.GetById;
using Jobby.Application.Features.ContactFeatures.Queries.GetList;
using Jobby.HttpApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ContactController : ApiController
{
    [HttpGet("{contactId:guid}", Name = "GetContact")]
    public async Task<ActionResult<GetContactResponse>> GetContact(Guid contactId)
    {
        var contactQuery = new GetContactDetailQuery(contactId);
        return Ok(await Sender.Send(contactQuery));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand command)
    {
        var createdContact = await Sender.Send(command);
        return CreatedAtAction(nameof(CreateContact), createdContact);
    }

    [HttpDelete("{contactId:guid}")]
    public async Task<IActionResult> DeleteContact(Guid contactId)
    {
        await Sender.Send(new DeleteContactCommand(contactId));
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContact([FromBody] UpdateContactCommand command)
    {
        var updatedContact =  await Sender.Send(command);
        return Ok(updatedContact);
    }
    
    [HttpGet("/contacts", Name = "ListContacts")]
    public async Task<ActionResult<List<GetContactResponse>>> GetAllContacts()
    {
        var contactQuery = new GetContactsQuery();
        return Ok(await Sender.Send(contactQuery));
    }
}
