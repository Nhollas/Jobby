using FluentValidation;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

public sealed class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
{
    public CreateBoardCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}
