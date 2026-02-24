using System;

namespace Edri.Domain.Commands.Readings.DeleteReading;

public sealed class DeleteReadingCommand : CommandBase
{
    private static readonly DeleteReadingCommandValidation s_validation = new();

    public DeleteReadingCommand(Guid readingId) : base(readingId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}