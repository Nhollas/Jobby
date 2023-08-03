using FluentValidation;
using Jobby.Domain.Static;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;
public sealed class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotNull().WithMessage("This property is required.");
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("This property cannot be empty.").When(command => command.Title != null);

        RuleFor(command => command.Type)
            .NotNull().WithMessage("This property is required.")
            .IsInEnum().WithMessage("The provided activity type is not valid.");

        // Guid is a value type, so we don't need any other checks than null.
        RuleFor(command => command.BoardId)
            .NotEmpty().WithMessage("This property is required.");

        // DateTime is a value type, so we don't need any other checks than null.
        RuleFor(command => command.StartDate)
            .NotNull().WithMessage("This property is required.");
    }
}
