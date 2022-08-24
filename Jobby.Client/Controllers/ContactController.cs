using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels.ContactViewModels;
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
    public PartialViewResult CreatePartial(CreateContactViewModel model)
    {
        return PartialView("_CreateContactPartial", model);
    }


    [HttpPost("Create")]
    public async Task<ActionResult> Create(CreateContactViewModel viewModel)
    {
        await _contactService.CreateContact(viewModel);

        return RedirectToRoute("Board/{boardId}/Job/{jobId}/Contacts", new { boardId = viewModel.BoardId, jobId = viewModel.JobId });
    }

    [HttpPost("UpdatePartial")]
    public PartialViewResult UpdatePartial(UpdateContactViewModel model)
    {
        return PartialView("_UpdateContactPartial", model);
    }

    [HttpPost("Update")]
    public async Task<ActionResult> Update(UpdateContactViewModel viewModel)
    {
        await _contactService.UpdateContact(viewModel);

        return RedirectToRoute("Board/{boardId}/Job/{jobId}/Contacts", new { boardId = viewModel.BoardId, jobId = viewModel.JobId });
    }

    [HttpPost("DeletePartial")]
    public PartialViewResult DeletePartial(DeleteContactViewModel model)
    {
        return PartialView("_DeleteContactPartial", model);
    }

    [HttpPost("Delete")]
    public async Task<ActionResult> Delete(DeleteContactViewModel viewModel)
    {
        await _contactService.DeleteContact(viewModel.ContactId);

        return RedirectToRoute("Board/{boardId}/Job/{jobId}/Contacts", new { boardId = viewModel.BoardId, jobId = viewModel.JobId });
    }
}
