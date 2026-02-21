using FluentValidation;

namespace Edri.Domain.Commands.Meters.DeleteMeter;

public sealed class DeleteMeterCommandValidation : AbstractValidator<DeleteMeterCommand>
{
    public DeleteMeterCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("METER_EMPTY_ID")
            .WithMessage("Meter id may not be empty");
    }
}