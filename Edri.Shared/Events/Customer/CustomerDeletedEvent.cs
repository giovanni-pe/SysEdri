using System;

namespace Edri.Shared.Events.Customer;

public sealed class CustomerDeletedEvent : DomainEvent
{
    public CustomerDeletedEvent(Guid customerId) : base(customerId)
    {
    }
}