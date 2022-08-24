using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.ContactViewModels;

namespace Jobby.Client.Interfaces;

public interface IContactFeaturesService
{
    Task<ApiResponse<Guid>> CreateContact(CreateContactViewModel model);
    Task DeleteContact(Guid id);
    Task UpdateContact(UpdateContactViewModel model);
}
