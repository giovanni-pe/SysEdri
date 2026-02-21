using FluentValidation;

namespace Edri.Domain.Commands.Companies.UpdateCompany;

public sealed class UpdateCompanyCommandValidation : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidation()
    {
        AddRuleForId();
        AddRuleForTaxId();
        AddRuleForBusinessName();
        AddRuleForEmail();
        AddRuleForPhone();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The Company Id is required.");
    }

    private void AddRuleForTaxId()
    {
        RuleFor(cmd => cmd.TaxId)
            .NotEmpty()
            .WithMessage("The Tax Id (RUC) is required.")
            .Length(11)
            .WithMessage("The Tax Id (RUC) must be exactly 11 characters.");
    }

    private void AddRuleForBusinessName()
    {
        RuleFor(cmd => cmd.BusinessName)
            .NotEmpty()
            .WithMessage("The Business Name is required.")
            .MaximumLength(200)
            .WithMessage("The Business Name must not exceed 200 characters.");
    }

    private void AddRuleForEmail()
    {
        RuleFor(cmd => cmd.Email)
            .EmailAddress()
            .When(cmd => !string.IsNullOrEmpty(cmd.Email))
            .WithMessage("The Email must be a valid email address.");
    }

    private void AddRuleForPhone()
    {
        RuleFor(cmd => cmd.Phone)
            .MaximumLength(20)
            .When(cmd => !string.IsNullOrEmpty(cmd.Phone))
            .WithMessage("The Phone must not exceed 20 characters.");
    }
}