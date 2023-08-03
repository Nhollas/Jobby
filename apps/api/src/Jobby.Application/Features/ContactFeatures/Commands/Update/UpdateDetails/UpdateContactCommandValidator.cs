using FluentValidation;

namespace Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;

public sealed class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
{
    public UpdateContactCommandValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(e => e.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}