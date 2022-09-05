using Jobby.Client.Contracts.Contact;
using Jobby.Client.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Client.Controllers;

[Authorize]
[Route("[controller]")]
public class ContactController : Controller
{
    private readonly IContactFeaturesService _contactService;

    public ContactController(IContactFeaturesService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost("CreatePartial")]
    public PartialViewResult CreatePartial(CreateContactRequest model)
    {
        return PartialView("_CreateContactPartial", model);
    }


    [HttpPost("Create")]
    public async Task<ActionResult> Create(CreateContactRequest viewModel)
    {
        await _contactService.CreateContact(viewModel);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{viewModel.JobId}/Contacts");
    }

    [HttpPost("UpdatePartial")]
    public PartialViewResult UpdatePartial(UpdateContactRequest model)
    {
        return PartialView("_UpdateContactPartial", model);
    }

    [HttpPost("Update")]
    public async Task<ActionResult> Update(UpdateContactRequest viewModel)
    {
        await _contactService.UpdateContact(viewModel);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{viewModel.JobId}/Contacts");
    }

    [HttpPost("DeletePartial")]
    public PartialViewResult DeletePartial(DeleteContactRequest model)
    {
        return PartialView("_DeleteContactPartial", model);
    }

    [HttpPost("Delete")]
    public async Task<ActionResult> Delete(DeleteContactRequest viewModel)
    {
        await _contactService.DeleteContact(viewModel.ContactId);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{viewModel.JobId}/Contacts");
    }
}
