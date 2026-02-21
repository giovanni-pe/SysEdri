using System;

namespace Edri.Domain.Commands.Customers.DeleteCustomer;

public sealed class DeleteCustomerCommand : CommandBase
{
    private static readonly DeleteCustomerCommandValidation s_validation = new();

    public DeleteCustomerCommand(Guid customerId) : base(customerId)
    {
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}