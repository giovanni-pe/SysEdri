using FluentValidation;

namespace Edri.Domain.Commands.Meters.CreateMeter;

public sealed class CreateMeterCommandValidation : AbstractValidator<CreateMeterCommand>
{
    public CreateMeterCommandValidation()
    {
        AddRuleForId();
        AddRuleForMeterNumber();
        AddRuleForSupplyId();
        AddRuleForType();
        AddRuleForInstallationDate();
        AddRuleForStatus();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The Meter Id is required.");
    }

    private void AddRuleForMeterNumber()
    {
        RuleFor(cmd => cmd.MeterNumber)
            .NotEmpty()
            .WithMessage("The Meter Number is required.")
            .MaximumLength(50)
            .WithMessage("The Meter Number must not exceed 50 characters.");
    }

    private void AddRuleForSupplyId()
    {
        RuleFor(cmd => cmd.SupplyId)
            .NotEmpty()
            .WithMessage("The Supply Id is required.");
    }

    private void AddRuleForType()
    {
        RuleFor(cmd => cmd.Type)
            .Must(type => type is 1 or 2 or 3)
            .WithMessage("The Type must be 1 (Analog), 2 (Digital), or 3 (Smart).");
    }

    private void AddRuleForInstallationDate()
    {
        RuleFor(cmd => cmd.InstallationDate)
            .NotEmpty()
            .WithMessage("The Installation Date is required.");
    }

    private void AddRuleForStatus()
    {
        RuleFor(cmd => cmd.Status)
            .Must(status => status is 1 or 2 or 3)
            .WithMessage("The Status must be 1 (Active), 2 (Retired), or 3 (Damaged).");
    }
}