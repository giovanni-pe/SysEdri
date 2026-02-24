using System;

namespace Edri.Domain.Commands.Captures.DeleteCapture;

public sealed class DeleteCaptureCommand : CommandBase
{
    private static readonly DeleteCaptureCommandValidation s_validation = new();

    public DeleteCaptureCommand(Guid captureId) : base(captureId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}