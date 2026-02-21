using System;

namespace Edri.Domain.Commands.Districts.CreateDistrict;

public sealed class CreateDistrictCommand : CommandBase
{
    private static readonly CreateDistrictCommandValidation s_validation = new();

    public string Name { get; }
    public string Code { get; }
    public Guid ProvinceId { get; }

    public CreateDistrictCommand(Guid DistrictId,Guid provinceId, string name, string code) : base(DistrictId)
    {
        Name = name;
        Code = code;
        ProvinceId = provinceId;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}