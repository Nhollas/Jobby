using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;
using static Jobby.Domain.Static.ContactConstants;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

internal sealed class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, BaseResult<CreateContactResponse, CreateContactOutcomes>>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IRepository<Board> _boardRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public CreateContactCommandHandler(
    IUserService userService,
    IRepository<Board> boardRepository,
    IDateTimeProvider dateTimeProvider,
    IRepository<Contact> contactRepository,
    IRepository<Job> jobRepository,
    IGuidProvider guidProvider,
    IMapper mapper)
    {
        _userId = userService.UserId();
        _boardRepository = boardRepository;
        _dateTimeProvider = dateTimeProvider;
        _contactRepository = contactRepository;
        _jobRepository = jobRepository;
        _guidProvider = guidProvider;
        _mapper = mapper;
    }

    public async Task<BaseResult<CreateContactResponse, CreateContactOutcomes>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateContactCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return new BaseResult<CreateContactResponse, CreateContactOutcomes>(
                IsSuccess: false,
                Outcome: CreateContactOutcomes.ValidationFailure,
                ValidationResult: validationResult
            );
        }
        
        ResourceResult<Board> boardResourceResult = null; // initialize board to null

        if (request.BoardId.HasValue)
        {
            Guid boardId = request.BoardId.Value;
            
            boardResourceResult = await ResourceProvider<Board>
                .GetBySpec(_boardRepository.FirstOrDefaultAsync)
                .ApplySpecification(new GetBoardWithJobsSpecification(boardId))
                .Check(_userId, cancellationToken);

            if (!boardResourceResult.IsSuccess)
            {
                return new BaseResult<CreateContactResponse, CreateContactOutcomes>(
                    IsSuccess: false,
                    Outcome: boardResourceResult.Outcome switch
                    {
                        Outcome.Unauthorised => CreateContactOutcomes.UnauthorizedBoardAccess,
                        Outcome.NotFound => CreateContactOutcomes.UnknownBoard,
                        _ => CreateContactOutcomes.UnknownError
                    },
                    ErrorMessage: boardResourceResult.ErrorMessage
                );
            }
        }

        var board = boardResourceResult?.Response;
        
        Contact createdContact = Contact.Create(
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.FirstName,
            request.LastName,
            request.JobTitle,
            request.Location,
            new Social(
                request.Socials.TwitterUrl,
                request.Socials.FacebookUrl,
                request.Socials.LinkedInUrl,
                request.Socials.GithubUrl
            ),
            board, // pass in the board if it's not null
            FormatCompanies(request.Companies),
            FormatEmails(request.Emails),
            FormatPhones(request.Phones));

        if (request.JobIds.Count > 0) // only link jobs to the contact if a board was retrieved
        {
            var jobsToLink = await _jobRepository.ListAsync(new GetJobsFromListSpecification(request.JobIds, _userId), cancellationToken);

            createdContact.SetJobs(jobsToLink);
        }

        await _contactRepository.AddAsync(createdContact, cancellationToken);

        return new BaseResult<CreateContactResponse, CreateContactOutcomes>(
            IsSuccess: true,
            Outcome: CreateContactOutcomes.ContactCreated,
            Response: _mapper.Map<CreateContactResponse>(createdContact)
        );
    }


    private List<Company> FormatCompanies(List<string> companies)
    {
        return (from string company in companies select new Company(_guidProvider.Create(), company))
            .ToList();
    }

    private List<Email> FormatEmails(List<EmailRequest> emails)
    {
        return (from EmailRequest email in emails select new Email(_guidProvider.Create(), email.Name, (EmailType)email.Type))
            .ToList();
    }

    private List<Phone> FormatPhones(List<PhoneRequest> phones)
    {
        return (from PhoneRequest phone in phones select new Phone(_guidProvider.Create(), phone.Number, (PhoneType)phone.Type))
            .ToList();
    }
}
