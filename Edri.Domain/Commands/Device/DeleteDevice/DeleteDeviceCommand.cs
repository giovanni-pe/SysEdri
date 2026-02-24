using System;

namespace Edri.Domain.Commands.Devices.DeleteDevice;

public sealed class DeleteDeviceCommand : CommandBase
{
    private static readonly DeleteDeviceCommandValidation s_validation = new();

    public DeleteDeviceCommand(Guid deviceId) : base(deviceId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}