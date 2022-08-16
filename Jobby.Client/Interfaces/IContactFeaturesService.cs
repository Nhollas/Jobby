using Jobby.Client.Services.Base;

namespace Jobby.Client.Interfaces;

public interface IContactFeaturesService
{
    Task<ApiResponse<Guid>> CreateContact(CreateContactCommand command);
    Task DeleteContact(Guid id);
    Task UpdateContact(UpdateContactCommand command);
}
