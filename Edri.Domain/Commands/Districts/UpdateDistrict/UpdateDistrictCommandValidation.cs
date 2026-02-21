using FluentValidation;

namespace Edri.Domain.Commands.Districts.UpdateDistrict;

public sealed class UpdateDistrictCommandValidation : AbstractValidator<UpdateDistrictCommand>
{
    public UpdateDistrictCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
        AddRuleForCode();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The District Id is required.");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithMessage("The District Name is required.")
            .MaximumLength(100)
            .WithMessage("The District Name must not exceed 100 characters.");
    }

    private void AddRuleForCode()
    {
        RuleFor(cmd => cmd.Code)
            .NotEmpty()
            .WithMessage("The District Code is required.")
            .Length(2)
            .WithMessage("The District Code must be exactly 2 characters (e.g., '15').");
    }
}