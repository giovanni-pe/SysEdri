using FluentValidation;

namespace Edri.Domain.Commands.Readings.UpdateReading;

public sealed class UpdateReadingCommandValidation : AbstractValidator<UpdateReadingCommand>
{
    public UpdateReadingCommandValidation()
    {
        AddRuleForId();
        AddRuleForDeviceId();
        AddRuleForMeterNumber();
        AddRuleForValueKwh();
        AddRuleForReadingDate();
        AddRuleForSource();
        AddRuleForOcrConfidence();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The Reading Id is required.");
    }

    private void AddRuleForDeviceId()
    {
        RuleFor(cmd => cmd.DeviceId)
            .NotEmpty()
            .WithMessage("The Device Id is required.");
    }

    private void AddRuleForMeterNumber()
    {
        RuleFor(cmd => cmd.MeterNumber)
            .NotEmpty()
            .WithMessage("The Meter Number is required.")
            .MaximumLength(50)
            .WithMessage("The Meter Number must not exceed 50 characters.");
    }

    private void AddRuleForValueKwh()
    {
        RuleFor(cmd => cmd.ValueKwh)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The Value in kWh must be greater than or equal to 0.");
    }

    private void AddRuleForReadingDate()
    {
        RuleFor(cmd => cmd.ReadingDate)
            .NotEmpty()
            .WithMessage("The Reading Date is required.");
    }

    private void AddRuleForSource()
    {
        RuleFor(cmd => cmd.Source)
            .Must(source => source is 1 or 2 or 3)
            .WithMessage("The Source must be 1 (OCR), 2 (Manual), or 3 (Estimated).");
    }

    private void AddRuleForOcrConfidence()
    {
        RuleFor(cmd => cmd.OcrConfidence)
            .InclusiveBetween(0, 100)
            .WithMessage("The OCR Confidence must be between 0 and 100.")
            .When(cmd => cmd.OcrConfidence.HasValue);
    }
}