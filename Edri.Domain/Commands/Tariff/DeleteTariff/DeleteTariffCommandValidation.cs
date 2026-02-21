using FluentValidation;

namespace Edri.Domain.Commands.Tariffs.DeleteTariff;

public sealed class DeleteTariffCommandValidation : AbstractValidator<DeleteTariffCommand>
{
    public DeleteTariffCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("TARIFF_EMPTY_ID")
            .WithMessage("Tariff id may not be empty");
    }
}