using AutoMapper;
using Jobby.Client.Contracts.Contact;
using Jobby.Client.Interfaces;
using Jobby.Client.Services.Base;

namespace Jobby.Client.Services;

public class ContactFeaturesService : BaseDataService, IContactFeaturesService
{
    private readonly IMapper _mapper;

    public ContactFeaturesService(IClient client, IMapper mapper) : base(client)
    {
        _mapper = mapper;
    }

    public async Task<ApiResponse<Guid>> CreateContact(CreateContactRequest model)
    {
        try
        {
            var command = _mapper.Map<CreateContactCommand>(model);

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

    public async Task UpdateContact(UpdateContactRequest model)
    {
        var command = _mapper.Map<UpdateContactCommand>(model);

        await _client.UpdateContactAsync(command);
    }
}
