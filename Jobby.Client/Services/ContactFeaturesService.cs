using Jobby.Client.Interfaces;
using Jobby.Client.Services.Base;

namespace Jobby.Client.Services;

public class ContactFeaturesService : BaseDataService, IContactFeaturesService
{
    public ContactFeaturesService(IClient client) : base(client)
    {
    }

    public async Task<ApiResponse<Guid>> CreateContact(CreateContactCommand command)
    {
        try
        {
            var newContactId = await _client.CreateContactAsync(command);
            return new ApiResponse<Guid>() { Data = newContactId, Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task DeleteContact(Guid id)
    {
        await _client.DeleteContactAsync(id);
    }

    public async Task UpdateContact(UpdateContactCommand command)
    {
        await _client.UpdateContactAsync(command);
    }
}
