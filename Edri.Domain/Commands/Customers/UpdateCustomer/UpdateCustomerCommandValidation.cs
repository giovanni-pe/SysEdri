using FluentValidation;

namespace Edri.Domain.Commands.Customers.UpdateCustomer;

public sealed class UpdateCustomerCommandValidation : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidation()
    {
        AddRuleForId();
        AddRuleForUserId();
        AddRuleForDistrictId();
        AddRuleForDocumentType();
        AddRuleForDocumentNumber();
        AddRuleForCustomerType();
        AddRuleForStatus();
        AddRuleForEmail();
        AddRuleForPhone();
    }

    private void AddRuleForId()
    {
        RuleFor(cmd => cmd.AggregateId)
            .NotEmpty()
            .WithMessage("The Customer Id is required.");
    }

    private void AddRuleForUserId()
    {
        RuleFor(cmd => cmd.UserId)
            .NotEmpty()
            .WithMessage("The User Id is required.");
    }

    private void AddRuleForDistrictId()
    {
        RuleFor(cmd => cmd.DistrictId)
            .NotEmpty()
            .WithMessage("The District Id is required.");
    }

    private void AddRuleForDocumentType()
    {
        RuleFor(cmd => cmd.DocumentType)
            .NotEmpty()
            .WithMessage("The Document Type is required.")
            .Must(dt => dt == "DNI" || dt == "RUC" || dt == "CE")
            .WithMessage("The Document Type must be DNI, RUC or CE.");
    }

    private void AddRuleForDocumentNumber()
    {
        RuleFor(cmd => cmd.DocumentNumber)
            .NotEmpty()
            .WithMessage("The Document Number is required.")
            .MaximumLength(20)
            .WithMessage("The Document Number must not exceed 20 characters.");
    }

    private void AddRuleForCustomerType()
    {
        RuleFor(cmd => cmd.CustomerType)
            .NotEmpty()
            .WithMessage("The Customer Type is required.")
            .Must(ct => ct == "Residential" || ct == "Commercial" || ct == "Industrial")
            .WithMessage("The Customer Type must be Residential, Commercial or Industrial.");
    }

    private void AddRuleForStatus()
    {
        RuleFor(cmd => cmd.Status)
            .NotEmpty()
            .WithMessage("The Status is required.")
            .Must(s => s == "Active" || s == "Suspended" || s == "Retired")
            .WithMessage("The Status must be Active, Suspended or Retired.");
    }

    private void AddRuleForEmail()
    {
        RuleFor(cmd => cmd.Email)
            .EmailAddress()
            .When(cmd => !string.IsNullOrEmpty(cmd.Email))
            .WithMessage("The Email must be a valid email address.");
    }

    private void AddRuleForPhone()
    {
        RuleFor(cmd => cmd.Phone)
            .MaximumLength(20)
            .When(cmd => !string.IsNullOrEmpty(cmd.Phone))
            .WithMessage("The Phone must not exceed 20 characters.");
    }
}