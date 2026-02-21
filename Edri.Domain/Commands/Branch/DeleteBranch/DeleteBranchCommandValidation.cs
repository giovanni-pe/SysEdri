using FluentValidation;

namespace Edri.Domain.Commands.Branches.DeleteBranch;

public sealed class DeleteBranchCommandValidation : AbstractValidator<DeleteBranchCommand>
{
    public DeleteBranchCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("BRANCH_EMPTY_ID")
            .WithMessage("Branch id may not be empty");
    }
}