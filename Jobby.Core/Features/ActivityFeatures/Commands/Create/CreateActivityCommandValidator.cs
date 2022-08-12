using FluentValidation;

namespace Jobby.Core.Features.ActivityFeatures.Commands.Create;
public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        RuleFor(e => e.ActivityType)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        RuleFor(e => e.BoardId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
        RuleFor(e => e.StartDate)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}
