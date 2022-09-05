using Jobby.Client.Contracts.Contact;
using Jobby.Client.Services.Base;

namespace Jobby.Client.Interfaces;

public interface IContactFeaturesService
{
    Task<ApiResponse<Guid>> CreateContact(CreateContactRequest model);
    Task DeleteContact(Guid id);
    Task UpdateContact(UpdateContactRequest model);
}
