using FluentValidation;

namespace Edri.Domain.Commands.Readings.DeleteReading;

public sealed class DeleteReadingCommandValidation : AbstractValidator<DeleteReadingCommand>
{
    public DeleteReadingCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("READING_EMPTY_ID")
            .WithMessage("Reading id may not be empty");
    }
}