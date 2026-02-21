using System;

namespace Edri.Domain.Commands.Provinces.UpdateProvince;

public sealed class UpdateProvinceCommand : CommandBase
{
    private static readonly UpdateProvinceCommandValidation s_validation = new();

    public Guid DepartmentId { get; }

    public string Name { get; }
    public string Code { get; }

    public UpdateProvinceCommand(Guid ProvinceId, Guid departmentId, string name, string code) : base(ProvinceId)
    {
        DepartmentId = departmentId;
        Name = name;
        Code = code;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}