using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Update;

internal sealed class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Unit>
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
            throw new NotFoundException($"A contact with id {request.ContactId} could not be found.");
        }

        if (contactToUpdate.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        contactToUpdate = _mapper.Map<Contact>(request);

        await _repository.UpdateAsync(contactToUpdate, cancellationToken);

        return Unit.Value;
    }

    //TODO: Review this method.
}
