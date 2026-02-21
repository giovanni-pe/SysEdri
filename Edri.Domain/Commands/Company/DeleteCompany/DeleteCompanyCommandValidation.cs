using FluentValidation;

namespace Edri.Domain.Commands.Companies.DeleteCompany;

public sealed class DeleteCompanyCommandValidation : AbstractValidator<DeleteCompanyCommand>
{
    public DeleteCompanyCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("COMPANY_EMPTY_ID")
            .WithMessage("Company id may not be empty");
    }
}