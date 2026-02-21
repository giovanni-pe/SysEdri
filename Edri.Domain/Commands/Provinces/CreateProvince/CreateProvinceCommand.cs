using System;

namespace Edri.Domain.Commands.Provinces.CreateProvince;

public sealed class CreateProvinceCommand : CommandBase
{
    private static readonly CreateProvinceCommandValidation s_validation = new();

    public string Name { get; }
    public string Code { get; }
    public Guid DepartmentId { get; }

    public CreateProvinceCommand(Guid ProvinceId,Guid departmentId, string name, string code) : base(ProvinceId)
    {
        Name = name;
        Code = code;
        DepartmentId = departmentId;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}