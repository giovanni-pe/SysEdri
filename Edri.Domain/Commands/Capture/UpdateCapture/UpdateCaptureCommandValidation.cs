using FluentValidation;

namespace Edri.Domain.Commands.Captures.UpdateCapture;

public sealed class UpdateCaptureCommandValidation : AbstractValidator<UpdateCaptureCommand>
{
    public UpdateCaptureCommandValidation()
    {
        AddRuleForId();
        AddRuleForDeviceId();
        AddRuleForTimestamp();
        AddRuleForImageUrl();
        AddRuleForStatus();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The Capture Id is required.");
    }

    private void AddRuleForDeviceId()
    {
        RuleFor(cmd => cmd.DeviceId)
            .NotEmpty()
            .WithMessage("The Device Id is required.");
    }

    private void AddRuleForTimestamp()
    {
        RuleFor(cmd => cmd.Timestamp)
            .NotEmpty()
            .WithMessage("The Timestamp is required.");
    }

    private void AddRuleForImageUrl()
    {
        RuleFor(cmd => cmd.ImageUrl)
            .NotEmpty()
            .WithMessage("The Image URL is required.")
            .MaximumLength(500)
            .WithMessage("The Image URL must not exceed 500 characters.");
    }

    private void AddRuleForStatus()
    {
        RuleFor(cmd => cmd.Status)
            .Must(status => status is 1 or 2 or 3)
            .WithMessage("The Status must be 1 (Pending), 2 (Processed), or 3 (Failed).");
    }
}