using System;

namespace Edri.Domain.Commands.Tariffs.DeleteTariff;

public sealed class DeleteTariffCommand : CommandBase
{
    private static readonly DeleteTariffCommandValidation s_validation = new();

    public DeleteTariffCommand(Guid tariffId) : base(tariffId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}