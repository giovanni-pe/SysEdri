using System;

namespace Edri.Domain.Commands.Branches.DeleteBranch;

public sealed class DeleteBranchCommand : CommandBase
{
    private static readonly DeleteBranchCommandValidation s_validation = new();

    public DeleteBranchCommand(Guid branchId) : base(branchId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}