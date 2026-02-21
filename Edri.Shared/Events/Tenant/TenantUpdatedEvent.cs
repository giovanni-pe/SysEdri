using System;

namespace Edri.Shared.Events.Tenant;

public sealed class TenantUpdatedEvent : DomainEvent
{
    public string Name { get; set; }

    public TenantUpdatedEvent(Guid tenantId, string name) : base(tenantId)
    {
        Name = name;
    }
}