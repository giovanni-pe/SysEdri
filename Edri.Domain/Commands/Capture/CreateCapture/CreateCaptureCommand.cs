using System;

namespace Edri.Domain.Commands.Captures.CreateCapture;

public sealed class CreateCaptureCommand : CommandBase
{
    private static readonly CreateCaptureCommandValidation s_validation = new();

    public Guid DeviceId { get; }
    public DateTime Timestamp { get; }
    public string ImageUrl { get; }
    public int Status { get; }

    public CreateCaptureCommand(
        Guid captureId,
        Guid deviceId,
        DateTime timestamp,
        string imageUrl,
        int status) : base(captureId)
    {
        DeviceId = deviceId;
        Timestamp = timestamp;
        ImageUrl = imageUrl;
        Status = status;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}