using System;

namespace Edri.Shared.Events.Tenant;

public sealed class TenantCreatedEvent : DomainEvent
{
    public string Name { get; set; }

    public TenantCreatedEvent(Guid tenantId, string name) : base(tenantId)
    {
        Name = name;
    }
}