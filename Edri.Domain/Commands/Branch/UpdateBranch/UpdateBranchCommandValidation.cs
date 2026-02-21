using FluentValidation;

namespace Edri.Domain.Commands.Branches.UpdateBranch;

public sealed class UpdateBranchCommandValidation : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidation()
    {
        AddRuleForId();
        AddRuleForCompanyId();
        AddRuleForDistrictId();
        AddRuleForName();
        AddRuleForPhone();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The Branch Id is required.");
    }

    private void AddRuleForCompanyId()
    {
        RuleFor(cmd => cmd.CompanyId)
            .NotEmpty()
            .WithMessage("The Company Id is required.");
    }

    private void AddRuleForDistrictId()
    {
        RuleFor(cmd => cmd.DistrictId)
            .NotEmpty()
            .WithMessage("The District Id is required.");
    }

    private void AddRuleForName()
    {
        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithMessage("The Branch Name is required.")
            .MaximumLength(100)
            .WithMessage("The Branch Name must not exceed 100 characters.");
    }

    private void AddRuleForPhone()
    {
        RuleFor(cmd => cmd.Phone)
            .MaximumLength(20)
            .When(cmd => !string.IsNullOrEmpty(cmd.Phone))
            .WithMessage("The Phone must not exceed 20 characters.");
    }
}