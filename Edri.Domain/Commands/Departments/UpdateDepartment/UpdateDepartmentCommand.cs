using System;

namespace Edri.Domain.Commands.Departments.UpdateDepartment;

public sealed class UpdateDepartmentCommand : CommandBase
{
    private static readonly UpdateDepartmentCommandValidation s_validation = new();

    public string Name { get; }
    public string Code { get; }

    public UpdateDepartmentCommand(Guid departmentId, string name, string code) : base(departmentId)
    {
        Name = name;
        Code = code;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}