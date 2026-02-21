using System;

namespace Edri.Domain.Commands.Districts.DeleteDistrict;

public sealed class DeleteDistrictCommand : CommandBase
{
    private static readonly DeleteDistrictCommandValidation s_validation = new();

    public DeleteDistrictCommand(Guid DistrictId) : base(DistrictId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}