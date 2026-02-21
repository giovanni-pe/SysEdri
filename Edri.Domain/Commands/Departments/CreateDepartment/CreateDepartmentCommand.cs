using System;

namespace Edri.Domain.Commands.Departments.CreateDepartment;

public sealed class CreateDepartmentCommand : CommandBase
{
    private static readonly CreateDepartmentCommandValidation s_validation = new();

    public string Name { get; }
    public string Code { get; }

    public CreateDepartmentCommand(Guid departmentId, string name, string code) : base(departmentId)
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