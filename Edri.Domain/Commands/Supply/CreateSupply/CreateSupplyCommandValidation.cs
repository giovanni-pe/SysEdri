using FluentValidation;

namespace Edri.Domain.Commands.Supplies.CreateSupply;

public sealed class CreateSupplyCommandValidation : AbstractValidator<CreateSupplyCommand>
{
    public CreateSupplyCommandValidation()
    {
        AddRuleForId();
        AddRuleForSupplyNumber();
        AddRuleForCustomerId();
        AddRuleForTariffId();
        AddRuleForBranchId();
        AddRuleForDistrictId();
        AddRuleForInstallationAddress();
        AddRuleForStatus();
        AddRuleForActivationDate();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The Supply Id is required.");
    }

    private void AddRuleForSupplyNumber()
    {
        RuleFor(cmd => cmd.SupplyNumber)
            .NotEmpty()
            .WithMessage("The Supply Number is required.")
            .MaximumLength(20)
            .WithMessage("The Supply Number must not exceed 20 characters.");
    }

    private void AddRuleForCustomerId()
    {
        RuleFor(cmd => cmd.CustomerId)
            .NotEmpty()
            .WithMessage("The Customer Id is required.");
    }

    private void AddRuleForTariffId()
    {
        RuleFor(cmd => cmd.TariffId)
            .NotEmpty()
            .WithMessage("The Tariff Id is required.");
    }

    private void AddRuleForBranchId()
    {
        RuleFor(cmd => cmd.BranchId)
            .NotEmpty()
            .WithMessage("The Branch Id is required.");
    }

    private void AddRuleForDistrictId()
    {
        RuleFor(cmd => cmd.DistrictId)
            .NotEmpty()
            .WithMessage("The District Id is required.");
    }

    private void AddRuleForInstallationAddress()
    {
        RuleFor(cmd => cmd.InstallationAddress)
            .NotEmpty()
            .WithMessage("The Installation Address is required.")
            .MaximumLength(300)
            .WithMessage("The Installation Address must not exceed 300 characters.");
    }

    private void AddRuleForStatus()
    {
        RuleFor(cmd => cmd.Status)
            .NotEmpty()
            .WithMessage("The Status is required.")
            .Must(status => status is "Active" or "Suspended" or "CutOff" or "Retired")
            .WithMessage("The Status must be Active, Suspended, CutOff, or Retired.");
    }

    private void AddRuleForActivationDate()
    {
        RuleFor(cmd => cmd.ActivationDate)
            .NotEmpty()
            .WithMessage("The Activation Date is required.");
    }
}