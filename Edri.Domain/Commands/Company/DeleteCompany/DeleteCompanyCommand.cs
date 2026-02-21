using System;

namespace Edri.Domain.Commands.Companies.DeleteCompany;

public sealed class DeleteCompanyCommand : CommandBase
{
    private static readonly DeleteCompanyCommandValidation s_validation = new();

    public DeleteCompanyCommand(Guid companyId) : base(companyId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}