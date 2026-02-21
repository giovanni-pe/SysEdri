using Edri.Domain.Errors;
using FluentValidation;

namespace Edri.Domain.Commands.Departments.DeleteDepartment;

public sealed class DeleteDepartmentCommandValidation : AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("DEPARTMENT_EMPTY_ID")
            .WithMessage("Department id may not be empty");
    }
}