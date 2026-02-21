using System;

namespace Edri.Domain.Commands.Districts.UpdateDistrict;

public sealed class UpdateDistrictCommand : CommandBase
{
    private static readonly UpdateDistrictCommandValidation s_validation = new();

    public Guid ProvinceId { get; }

    public string Name { get; }
    public string Code { get; }

    public UpdateDistrictCommand(Guid DistrictId, Guid provinceId, string name, string code) : base(DistrictId)
    {
        ProvinceId = provinceId;
        Name = name;
        Code = code;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}