using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Features.JobFeatures.Specifications;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;
using static Jobby.Domain.Static.ContactConstants;

namespace Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;

internal class UpdateContactCommandHandler(
    IUserService userService,
    TimeProvider timeProvider,
    IRepository<Contact> contactRepository,
    IRepository<Board> boardRepository,
    IRepository<Job> jobRepository,
    IMapper mapper)
    : IRequestHandler<UpdateContactCommand, IDispatchResult<ContactDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<ContactDto>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        Contact? contact = await contactRepository.FirstOrDefaultAsync(new GetContactWithSocialsSpecification(request.ContactReference), cancellationToken);
        
        if (contact is null)
        {
            return DispatchResults.NotFound<ContactDto>(request.ContactReference);
        }

        if (!contact.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<ContactDto>(contact.Reference);
        }
        
        contact.Update(
            request.FirstName,
            request.LastName,
            request.JobTitle,
            request.Location,
            new Social(
                request.Socials.TwitterUrl,
                request.Socials.FacebookUrl,
                request.Socials.LinkedInUrl,
                request.Socials.GithubUrl));
        
        if (request.BoardReference != string.Empty && request.BoardReference != contact.BoardReference)
        {
            Board? board = await boardRepository.GetByReferenceAsync(request.BoardReference, cancellationToken);
            
            if (board is null)
            {
                return DispatchResults.NotFound<ContactDto>(request.BoardReference);
            }
            
            if (!board.IsOwnedBy(_userId))
            {
                return DispatchResults.Unauthorized<ContactDto>(
                    board.Reference);
            }
            
            contact.SetBoard(board);
        }

        List<string> contactJobIds = contact.JobContacts.Select(x => x.Job.Reference).ToList();
        
        if (request.JobReferences.Count > 0 && !contactJobIds.SequenceEqual(request.JobReferences))
        {
            List<Job> jobsToLink = await jobRepository.ListAsync(new GetJobsFromIdsSpecification(request.JobReferences, _userId), cancellationToken);

            contact.SetJobs(jobsToLink);
        }

        if (request.Companies.Count > 0)
        {
            List<Company> updatedCompanies = request.Companies
                .Select(x => Company.Create(timeProvider.GetUtcNow(), _userId, x.Name, contact))
                .ToList();

            contact.UpdateCompanies(updatedCompanies);
        }

        if (request.Emails.Count > 0)
        {
            List<Email> updatedEmails = request.Emails
                .Select(x => Email.Create(timeProvider.GetUtcNow(), _userId, x.Name, (EmailType)x.Type, contact))
                .ToList();

            contact.UpdateEmails(updatedEmails);
        }

        if (request.Phones.Count > 0)
        {
            List<Phone> updatedPhones = request.Phones
                .Select(x => Phone.Create(timeProvider.GetUtcNow(), _userId, x.Number, (PhoneType)x.Type, contact))
                .ToList();

            contact.UpdatePhones(updatedPhones);
        }

        contact.UpdateEntity(timeProvider.GetUtcNow());

        await contactRepository.SaveChangesAsync(cancellationToken);
        
        return DispatchResults.Ok(mapper.Map<ContactDto>(contact));
    }
}
