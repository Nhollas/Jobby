using AutoMapper;
using FluentValidation.Results;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Create;

internal class CreateContactCommandHandler(
    IUserService userService,
    IRepository<Board> boardRepository,
    TimeProvider timeProvider,
    IRepository<Contact> contactRepository,
    IRepository<Job> jobRepository,
    IMapper mapper)
    : IRequestHandler<CreateContactCommand, IDispatchResult<ContactDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<ContactDto>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        CreateContactCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return DispatchResults.UnprocessableEntity<ContactDto>(validationResult);
        }
        
        Board? board = await boardRepository.FirstOrDefaultAsync(new GetBoardWithJobsSpecification(request.BoardReference), cancellationToken);
        
        if (board is null)
        {
            return DispatchResults.NotFound<ContactDto>(request.BoardReference);
        }
        
        if (!board.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<ContactDto>(board.Reference);
        }
        
        Contact createdContact = board.AddContact(
            timeProvider,
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
            request.Companies,
            request.Emails,
            request.Phones);

        if (request.JobReferences.Count > 0)
        {
            List<Job> jobsToLink = await jobRepository.ListAsync(new GetJobsFromListSpecification(request.JobReferences, _userId), cancellationToken);

            createdContact.SetJobs(jobsToLink);
        }

        await contactRepository.AddAsync(createdContact, cancellationToken);
        
        return DispatchResults.Ok(mapper.Map<ContactDto>(createdContact));
    }
}
