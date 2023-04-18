using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;
using static Jobby.Domain.Static.ContactConstants;

namespace Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;

internal sealed class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Unit>
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly IRepository<Board> _boardRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly string _userId;

    public UpdateContactCommandHandler(
        IUserService userService,
        IDateTimeProvider timeProvider,
        IRepository<Contact> contactRepository,
        IRepository<Board> boardRepository)
    {
        _userId = userService.UserId();
        _timeProvider = timeProvider;
        _contactRepository = contactRepository;
        _boardRepository = boardRepository;
    }

    public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        Contact contactToUpdate = await ResourceProvider<Contact>
            .GetBySpec(_contactRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetContactWithSocialsSpecification(request.ContactId))
            .Check(_userId, cancellationToken);

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

        if (request.JobIds.Count > 0 && contactToUpdate.BoardId.HasValue)
        {
            Guid boardId = contactToUpdate.BoardId.Value;
            
            Board board = await ResourceProvider<Board>
                .GetBySpec(_boardRepository.FirstOrDefaultAsync)
                .ApplySpecification(new GetBoardWithJobsSpecification(boardId))
                .Check(_userId, cancellationToken);

            if (!board.BoardOwnsJobs(request.JobIds))
            {
                throw new NotFoundException($"The {nameof(List<Job>)} {request.JobIds} you wanted to link doesn't exist in the Board {contactToUpdate.Board.Id}.");
            }

            var jobsToLink = board.JobLists
                .SelectMany(x => x.Jobs)
                .Where(x => request.JobIds.Contains(x.Id))
                .ToList();

            contactToUpdate.SetJobs(jobsToLink);
        }

        if (request.Companies.Count > 0)
        {
            var updatedCompanies = request.Companies
                .Select(x => new Company(Guid.NewGuid(), x.Name))
                .ToList();

            contactToUpdate.UpdateCompanies(updatedCompanies);
        }

        if (request.Emails.Count > 0)
        {
            var updatedEmails = request.Emails
                .Select(x => new Email(Guid.NewGuid(), x.Name, (EmailType)x.Type))
                .ToList();

            contactToUpdate.UpdateEmails(updatedEmails);
        }

        if (request.Phones.Count > 0)
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
}
