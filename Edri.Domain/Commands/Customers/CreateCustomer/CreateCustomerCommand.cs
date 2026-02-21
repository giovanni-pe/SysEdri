using System;

namespace Edri.Domain.Commands.Customers.CreateCustomer;

public sealed class CreateCustomerCommand : CommandBase
{
    private static readonly CreateCustomerCommandValidation s_validation = new();

    public Guid UserId { get; }
    public Guid DistrictId { get; }
    public string DocumentType { get; }
    public string DocumentNumber { get; }
    public string CustomerType { get; }
    public string? BusinessName { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public string? Address { get; }
    public string? Phone { get; }
    public string? Email { get; }
    public string Status { get; }

    public CreateCustomerCommand(
        Guid customerId,
        Guid userId,
        Guid districtId,
        string documentType,
        string documentNumber,
        string customerType,
        string? businessName,
        string? firstName,
        string? lastName,
        string? address,
        string? phone,
        string? email,
        string status) : base(customerId)
    {
        UserId = userId;
        DistrictId = districtId;
        DocumentType = documentType;
        DocumentNumber = documentNumber;
        CustomerType = customerType;
        BusinessName = businessName;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Phone = phone;
        Email = email;
        Status = status;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}