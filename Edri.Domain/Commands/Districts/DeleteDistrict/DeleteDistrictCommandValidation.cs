using Edri.Domain.Errors;
using FluentValidation;

namespace Edri.Domain.Commands.Districts.DeleteDistrict;

public sealed class DeleteDistrictCommandValidation : AbstractValidator<DeleteDistrictCommand>
{
    public DeleteDistrictCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("District_EMPTY_ID")
            .WithMessage("District id may not be empty");
    }
}