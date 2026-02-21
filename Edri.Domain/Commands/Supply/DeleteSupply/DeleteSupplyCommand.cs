using System;

namespace Edri.Domain.Commands.Supplies.DeleteSupply;

public sealed class DeleteSupplyCommand : CommandBase
{
    private static readonly DeleteSupplyCommandValidation s_validation = new();

    public DeleteSupplyCommand(Guid supplyId) : base(supplyId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}