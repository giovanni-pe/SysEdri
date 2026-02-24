using FluentValidation;

namespace Edri.Domain.Commands.Devices.DeleteDevice;

public sealed class DeleteDeviceCommandValidation : AbstractValidator<DeleteDeviceCommand>
{
    public DeleteDeviceCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("DEVICE_EMPTY_ID")
            .WithMessage("Device id may not be empty");
    }
}