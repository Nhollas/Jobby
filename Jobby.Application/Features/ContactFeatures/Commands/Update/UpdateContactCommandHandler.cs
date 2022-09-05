using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Update;

internal sealed class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Unit>
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly IUserService _userService;
    private readonly string _userId;

    public UpdateContactCommandHandler(
        IUserService userService,
        IDateTimeProvider timeProvider,
        IRepository<Contact> contactRepository)
    {
        _userService = userService;
        _userId = _userService.UserId();
        _timeProvider = timeProvider;
        _contactRepository = contactRepository;
    }

    public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contactSpec = new UpdateContactSpec(request.ContactId);

        Contact contactToUpdate = await _contactRepository.FirstOrDefaultAsync(contactSpec, cancellationToken);

        if (contactToUpdate is null)
        {
            throw new NotFoundException($"A contact with id {request.ContactId} could not be found.");
        }

        if (contactToUpdate.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        contactToUpdate.Update(
            request.FirstName,
            request.LastName,
            request.JobTitle,
            request.Location,
            new Social(
                request.Socials.TwitterUrl,
                request.Socials.FacebookUrl,
                request.Socials.LinkedInUrl,
                request.Socials.GithubUrl));

        if (request.JobIds != null)
        {
            if (!BoardOwnsJob(contactToUpdate.Board, request.JobIds))
            {
                throw new NotFoundException($"The {nameof(List<Job>)} {request.JobIds} you wanted to link doesn't exist in the Board {contactToUpdate.Board.Id}.");
            }

            List<Job> jobsToLink = contactToUpdate.Board.JobList
                .SelectMany(x => x.Jobs)
                .Where(x => request.JobIds.Contains(x.Id))
                .ToList();

            contactToUpdate.SetJobs(jobsToLink);
        }

        if (request.Companies != null)
        {
            var updatedCompanies = request.Companies
                .Select(x => new Company(Guid.NewGuid(), x.Name))
                .ToList();

            contactToUpdate.UpdateCompanies(updatedCompanies);
        }

        if (request.Emails != null)
        {
            var updatedEmails = request.Emails
                .Select(x => new Email(Guid.NewGuid(), x.Name))
                .ToList();

            contactToUpdate.UpdateEmails(updatedEmails);
        }

        if (request.Phones != null)
        {
            var updatedPhones = request.Phones
                .Select(x => new Phone(Guid.NewGuid(), x.Number, (PhoneType)x.Type))
                .ToList();

            contactToUpdate.UpdatePhones(updatedPhones);
        }

        contactToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _contactRepository.UpdateAsync(contactToUpdate, cancellationToken);

        return Unit.Value;
    }

    private static bool BoardOwnsJob(Board board, List<Guid> jobIds)
    {
        return board.JobList
            .SelectMany(x => x.Jobs
            .Where(x => jobIds.Contains(x.Id)))
            .Any();
    }
}
