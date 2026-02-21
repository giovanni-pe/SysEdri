using Edri.Domain.Errors;
using FluentValidation;

namespace Edri.Domain.Commands.Customers.DeleteCustomer;

public sealed class DeleteCustomerCommandValidation : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidation()
    {
        AddRuleForId();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithErrorCode("CUSTOMER_EMPTY_ID")
            .WithMessage("Customer id may not be empty");
    }
}