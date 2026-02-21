using Edri.Domain.Errors;
using FluentValidation;

namespace Edri.Domain.Commands.Provinces.DeleteProvince;

public sealed class DeleteProvinceCommandValidation : AbstractValidator<DeleteProvinceCommand>
{
    public DeleteProvinceCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("Province_EMPTY_ID")
            .WithMessage("Province id may not be empty");
    }
}