using System;

namespace Edri.Domain.Commands.Provinces.DeleteProvince;

public sealed class DeleteProvinceCommand : CommandBase
{
    private static readonly DeleteProvinceCommandValidation s_validation = new();

    public DeleteProvinceCommand(Guid ProvinceId) : base(ProvinceId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}