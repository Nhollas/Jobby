using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Features.JobFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;
using static Jobby.Domain.Static.ContactConstants;

namespace Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;

internal sealed class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, BaseResult<ContactDto, UpdateContactOutcomes>>
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly IRepository<Board> _boardRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IDateTimeProvider _timeProvider;
    private readonly string _userId;
    private readonly IMapper _mapper;

    public UpdateContactCommandHandler(
        IUserService userService,
        IDateTimeProvider timeProvider,
        IRepository<Contact> contactRepository,
        IRepository<Board> boardRepository,
        IRepository<Job> jobRepository,
        IMapper mapper)
    {
        _userId = userService.UserId();
        _timeProvider = timeProvider;
        _contactRepository = contactRepository;
        _boardRepository = boardRepository;
        _jobRepository = jobRepository;
        _mapper = mapper;
    }

    public async Task<BaseResult<ContactDto, UpdateContactOutcomes>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contactResourceResult = await ResourceProvider<Contact>
            .GetBySpec(_contactRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetContactWithSocialsSpecification(request.ContactReference))
            .Check(_userId, cancellationToken);

        if (!contactResourceResult.IsSuccess)
        {
            return new BaseResult<ContactDto, UpdateContactOutcomes>(
                IsSuccess: false,
                Outcome: contactResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => UpdateContactOutcomes.UnauthorizedContactAccess,
                    Outcome.NotFound => UpdateContactOutcomes.UnknownContact,
                    _ => UpdateContactOutcomes.UnknownError
                },
                ErrorMessage: contactResourceResult.ErrorMessage
            );
        }
        
        var contactToUpdate = contactResourceResult.Response;

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
        
        if (request.BoardReference != string.Empty && request.BoardReference != contactToUpdate.BoardReference)
        {
            var boardResourceResult = await ResourceProvider<Board>
                .GetByReference(_boardRepository.GetByReferenceAsync)
                .Check(_userId, request.BoardReference, cancellationToken);
            
            if (!boardResourceResult.IsSuccess)
            {
                return new BaseResult<ContactDto, UpdateContactOutcomes>(
                    IsSuccess: false,
                    Outcome: boardResourceResult.Outcome switch
                    {
                        Outcome.Unauthorised => UpdateContactOutcomes.UnauthorizedBoardAccess,
                        Outcome.NotFound => UpdateContactOutcomes.UnknownBoard,
                        _ => UpdateContactOutcomes.UnknownError
                    },
                    ErrorMessage: boardResourceResult.ErrorMessage
                );
            }
            
            var newBoard = boardResourceResult.Response;
            
            contactToUpdate.SetBoard(newBoard);
        }

        var contactJobIds = contactToUpdate.JobContacts.Select(x => x.Job.Reference).ToList();
        
        if (request.JobReferences.Count > 0 && !contactJobIds.SequenceEqual(request.JobReferences))
        {
            var jobsToLink = await _jobRepository.ListAsync(new GetJobsFromIdsSpecification(request.JobReferences, _userId), cancellationToken);

            contactToUpdate.SetJobs(jobsToLink);
        }

        if (request.Companies.Count > 0)
        {
            var updatedCompanies = request.Companies
                .Select(x => Company.Create(Guid.NewGuid(), _timeProvider.UtcNow, _userId, x.Name, contactToUpdate))
                .ToList();

            contactToUpdate.UpdateCompanies(updatedCompanies);
        }

        if (request.Emails.Count > 0)
        {
            var updatedEmails = request.Emails
                .Select(x => Email.Create(Guid.NewGuid(), _timeProvider.UtcNow, _userId, x.Name, (EmailType)x.Type, contactToUpdate))
                .ToList();

            contactToUpdate.UpdateEmails(updatedEmails);
        }

        if (request.Phones.Count > 0)
        {
            var updatedPhones = request.Phones
                .Select(x => Phone.Create(Guid.NewGuid(), _timeProvider.UtcNow, _userId, x.Number, (PhoneType)x.Type, contactToUpdate))
                .ToList();

            contactToUpdate.UpdatePhones(updatedPhones);
        }

        contactToUpdate.UpdateEntity(_timeProvider.UtcNow);

        await _contactRepository.SaveChangesAsync(cancellationToken);
        
        return new BaseResult<ContactDto, UpdateContactOutcomes>(
            IsSuccess: true,
            Outcome: UpdateContactOutcomes.ContactUpdated,
            Response: _mapper.Map<ContactDto>(contactToUpdate)
        );
    }
}
