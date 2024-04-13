using AutoMapper;
using FluentValidation.Results;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

internal sealed class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, BaseResult<ContactDto, CreateContactOutcomes>>
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

    public async Task<BaseResult<ContactDto, CreateContactOutcomes>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        CreateContactCommandValidator validator = new CreateContactCommandValidator();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return new BaseResult<ContactDto, CreateContactOutcomes>(
                IsSuccess: false,
                Outcome: CreateContactOutcomes.ValidationFailure,
                ValidationResult: validationResult
            );
        }
        
        ResourceResult<Board> boardResourceResult = null; // initialize board to null

        if (request.BoardReference != null)
        {
            boardResourceResult = await ResourceProvider<Board>
                .GetBySpec(_boardRepository.FirstOrDefaultAsync)
                .WithResource(request.BoardReference)
                .ApplySpecification(new GetBoardWithJobsSpecification(request.BoardReference))
                .Check(_userId, cancellationToken);

            if (!boardResourceResult.IsSuccess)
            {
                return new BaseResult<ContactDto, CreateContactOutcomes>(
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

        Board board = boardResourceResult?.Response;
        
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
            request.Companies,
            request.Emails,
            request.Phones);

        if (request.JobReferences.Count > 0) // only link jobs to the contact if a board was retrieved
        {
            List<Job> jobsToLink = await _jobRepository.ListAsync(new GetJobsFromListSpecification(request.JobReferences, _userId), cancellationToken);

            createdContact.SetJobs(jobsToLink);
        }

        await _contactRepository.AddAsync(createdContact, cancellationToken);

        return new BaseResult<ContactDto, CreateContactOutcomes>(
            IsSuccess: true,
            Outcome: CreateContactOutcomes.ContactCreated,
            Response: _mapper.Map<ContactDto>(createdContact)
        );
    }
}
