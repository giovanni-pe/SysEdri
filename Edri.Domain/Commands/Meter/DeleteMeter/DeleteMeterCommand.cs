using System;

namespace Edri.Domain.Commands.Meters.DeleteMeter;

public sealed class DeleteMeterCommand : CommandBase
{
    private static readonly DeleteMeterCommandValidation s_validation = new();

    public DeleteMeterCommand(Guid meterId) : base(meterId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}