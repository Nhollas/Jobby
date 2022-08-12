using AutoMapper;
using Jobby.Core.Entities;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.ContactFeatures.Commands.Update;

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Unit>
{
    private readonly IRepository<Contact> _repository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public UpdateContactCommandHandler(
        IRepository<Contact> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        Contact contactToUpdate = await _repository.GetByIdAsync(request.ContactId, cancellationToken);

        if (contactToUpdate == null)
        {
            // TODO: NotFound Problem Details.
        }

        if (contactToUpdate.OwnerId != _userId)
        {
            // TODO: NotAuthorized Problem Details.
        }

        contactToUpdate = _mapper.Map<Contact>(request);

        string[] convertedJobIds = request.JobIds.Select(g => g.ToString()).ToArray();

        // contactToUpdate.Join(convertedJobIds, request.Emails, request.Phones);

        await _repository.UpdateAsync(contactToUpdate, cancellationToken);

        return Unit.Value;
    }
}
