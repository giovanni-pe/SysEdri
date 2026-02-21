using System;

namespace Edri.Domain.Commands.Departments.DeleteDepartment;

public sealed class DeleteDepartmentCommand : CommandBase
{
    private static readonly DeleteDepartmentCommandValidation s_validation = new();

    public DeleteDepartmentCommand(Guid DepartmentId) : base(DepartmentId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}