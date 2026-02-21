using System;

namespace Edri.Shared.Events.Customer;

public sealed class CustomerUpdatedEvent : DomainEvent
{
    public string DocumentNumber { get; set; }

    public CustomerUpdatedEvent(
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