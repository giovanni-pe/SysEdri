using FluentValidation;

namespace Edri.Domain.Commands.Tariffs.CreateTariff;

public sealed class CreateTariffCommandValidation : AbstractValidator<CreateTariffCommand>
{
    public CreateTariffCommandValidation()
    {
        AddRuleForId();
        AddRuleForCompanyId();
        AddRuleForCode();
        AddRuleForName();
        AddRuleForPricePerKwh();
        AddRuleForFixedCharge();
        AddRuleForEffectiveFrom();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The Tariff Id is required.");
    }

    private void AddRuleForCompanyId()
    {
        RuleFor(cmd => cmd.CompanyId)
            .NotEmpty()
            .WithMessage("The Company Id is required.");
    }

    private void AddRuleForCode()
    {
        RuleFor(cmd => cmd.Code)
            .NotEmpty()
            .WithMessage("The Tariff Code is required.")
            .MaximumLength(20)
            .WithMessage("The Tariff Code must not exceed 20 characters.");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithMessage("The Tariff Name is required.")
            .MaximumLength(100)
            .WithMessage("The Tariff Name must not exceed 100 characters.");
    }

    private void AddRuleForPricePerKwh()
    {
        RuleFor(cmd => cmd.PricePerKwh)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The Price per Kwh must be greater than or equal to 0.");
    }

    private void AddRuleForFixedCharge()
    {
        RuleFor(cmd => cmd.FixedCharge)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The Fixed Charge must be greater than or equal to 0.");
    }

    private void AddRuleForEffectiveFrom()
    {
        RuleFor(cmd => cmd.EffectiveFrom)
            .NotEmpty()
            .WithMessage("The Effective From date is required.");
    }
}