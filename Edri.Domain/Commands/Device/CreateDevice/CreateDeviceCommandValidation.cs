using FluentValidation;

namespace Edri.Domain.Commands.Devices.CreateDevice;

public sealed class CreateDeviceCommandValidation : AbstractValidator<CreateDeviceCommand>
{
    public CreateDeviceCommandValidation()
    {
        AddRuleForId();
        AddRuleForCode();
        AddRuleForMeterId();
        AddRuleForBranchId();
        AddRuleForMacAddress();
        AddRuleForStatus();
        AddRuleForInstallationDate();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The Device Id is required.");
    }

    private void AddRuleForCode()
    {
        RuleFor(cmd => cmd.Code)
            .NotEmpty()
            .WithMessage("The Device Code is required.")
            .MaximumLength(50)
            .WithMessage("The Device Code must not exceed 50 characters.");
    }

    private void AddRuleForMeterId()
    {
        RuleFor(cmd => cmd.MeterId)
            .NotEmpty()
            .WithMessage("The Meter Id is required.");
    }

    private void AddRuleForBranchId()
    {
        RuleFor(cmd => cmd.BranchId)
            .NotEmpty()
            .WithMessage("The Branch Id is required.");
    }

    private void AddRuleForMacAddress()
    {
        RuleFor(cmd => cmd.MacAddress)
            .MaximumLength(17)
            .WithMessage("The MAC Address must not exceed 17 characters.")
            .When(cmd => !string.IsNullOrEmpty(cmd.MacAddress));
    }

    private void AddRuleForStatus()
    {
        RuleFor(cmd => cmd.Status)
            .Must(status => status is 1 or 2 or 3)
            .WithMessage("The Status must be 1 (Active), 2 (Inactive), or 3 (Maintenance).");
    }

    private void AddRuleForInstallationDate()
    {
        RuleFor(cmd => cmd.InstallationDate)
            .NotEmpty()
            .WithMessage("The Installation Date is required.");
    }
}