using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
internal sealed class GetContactDetailQueryHandler : IRequestHandler<GetContactDetailQuery, BaseResult<ContactDto, GetContactDetailOutcomes>>
{
    private readonly IReadRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public GetContactDetailQueryHandler(
        IUserService userService,
        IMapper mapper,
        IReadRepository<Contact> contactRepository)
    {
        _userId = userService.UserId();
        _mapper = mapper;
        _contactRepository = contactRepository;
    }

    public async Task<BaseResult<ContactDto, GetContactDetailOutcomes>> Handle(GetContactDetailQuery request, CancellationToken cancellationToken)
    {
        var contactResourceResult = await ResourceProvider<Contact>
            .GetBySpec(_contactRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetContactWithRelationshipsSpecification(request.ContactId))
            .Check(_userId, cancellationToken);

        if (!contactResourceResult.IsSuccess)
        {
            return new BaseResult<ContactDto, GetContactDetailOutcomes>(
                IsSuccess: false,
                Outcome: contactResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => GetContactDetailOutcomes.UnauthorizedContactAccess,
                    Outcome.NotFound => GetContactDetailOutcomes.UnknownContact,
                    _ => GetContactDetailOutcomes.UnknownError
                },
                ErrorMessage: contactResourceResult.ErrorMessage
            );
        }

        return new BaseResult<ContactDto, GetContactDetailOutcomes>(
            IsSuccess: true,
            Outcome: GetContactDetailOutcomes.ContactFound,
            Response: _mapper.Map<ContactDto>(contactResourceResult.Response)
        );
    }
}
