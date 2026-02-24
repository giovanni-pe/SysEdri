using FluentValidation;

namespace Edri.Domain.Commands.Captures.DeleteCapture;

public sealed class DeleteCaptureCommandValidation : AbstractValidator<DeleteCaptureCommand>
{
    public DeleteCaptureCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("CAPTURE_EMPTY_ID")
            .WithMessage("Capture id may not be empty");
    }
}