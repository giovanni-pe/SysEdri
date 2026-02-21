using FluentValidation;

namespace Edri.Domain.Commands.Supplies.DeleteSupply;

public sealed class DeleteSupplyCommandValidation : AbstractValidator<DeleteSupplyCommand>
{
    public DeleteSupplyCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("SUPPLY_EMPTY_ID")
            .WithMessage("Supply id may not be empty");
    }
}