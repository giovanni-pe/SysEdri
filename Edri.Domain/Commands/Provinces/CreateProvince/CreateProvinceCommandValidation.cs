using FluentValidation;

namespace Edri.Domain.Commands.Provinces.CreateProvince;

public sealed class CreateProvinceCommandValidation : AbstractValidator<CreateProvinceCommand>
{
    public CreateProvinceCommandValidation()
    {
        AddRuleForId();
        AddRuleForName();
        AddRuleForCode();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The Province Id is required.");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithMessage("The Province Name is required.")
            .MaximumLength(100)
            .WithMessage("The Province Name must not exceed 100 characters.");
    }

    private void AddRuleForCode()
    {
        RuleFor(cmd => cmd.Code)
            .NotEmpty()
            .WithMessage("The Province Code is required.")
            .Length(2)
            .WithMessage("The Province Code must be exactly 2 characters (e.g., '15').");
    }
}