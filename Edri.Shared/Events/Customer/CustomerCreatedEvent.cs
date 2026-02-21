using System;

namespace Edri.Shared.Events.Customer;

public sealed class CustomerCreatedEvent : DomainEvent
{
    public string DocumentNumber { get; set; }

    public CustomerCreatedEvent(
        Guid customerId,
        Guid userId,
        Guid districtId,
        string documentType,
        string documentNumber,
        string customerType,
        string? businessName,
        string? firstName,
        string? lastName,
        string status) : base(customerId)
    {
        DocumentNumber = documentNumber;
    }
}